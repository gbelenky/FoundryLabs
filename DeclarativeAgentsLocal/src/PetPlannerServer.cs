using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;

namespace PetPlanner;

/// <summary>
/// Pet Planner MCP Server - .NET Implementation
/// Provides weather, activities, locations, and pet care tips for the Pet Planner agent.
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.Error.WriteLine("Pet Planner MCP Server starting...");
        Console.Error.WriteLine("Available tools: GetWeather, GetPetActivities, FindPetFriendlyLocations, GetPetCareTips");
        Console.Error.WriteLine("Server ready for connections...");

        var builder = Host.CreateApplicationBuilder(args);
        builder.Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();
        
        await builder.Build().RunAsync();
    }
}

/// <summary>
/// Pet Planner tools for MCP server
/// </summary>
[McpServerToolType]
public static class PetPlannerTools
{
    private static readonly Random _random = new();

    [McpServerTool(Name = "GetWeather"), Description("Get current weather information for a specific location")]
    public static string GetWeather(
        [Description("The location to get the weather for (city, state or city, country)")] string location)
    {
        try
        {
            var weatherConditions = new List<WeatherData>
            {
                new("sunny", 75, 45, 8, true),
                new("partly cloudy", 68, 55, 12, true),
                new("cloudy", 62, 70, 15, true),
                new("light rain", 58, 85, 18, false),
                new("heavy rain", 55, 95, 25, false),
                new("snow", 32, 80, 20, false)
            };

            var weather = weatherConditions[_random.Next(weatherConditions.Count)];
            var petAdvisory = weather.PetFriendly ? "Great for pets!" : "Keep pets indoors";

            return $"""
                Weather in {location}:
                Temperature: {weather.Temp}°F
                Conditions: {char.ToUpper(weather.Condition[0]) + weather.Condition[1..]}
                Wind: {weather.Wind} mph
                Humidity: {weather.Humidity}%
                Pet Advisory: {petAdvisory}
                """;
        }
        catch (Exception)
        {
            return $"Sorry, I couldn't get weather information for {location}. Please try again.";
        }
    }

    [McpServerTool(Name = "GetPetActivities"), Description("Get activity recommendations based on weather and pet type")]
    public static string GetPetActivities(
        [Description("Current weather condition (sunny, cloudy, rainy, etc.)")] string weatherCondition, 
        [Description("Type of pet (dog, cat, bird, etc.)")] string petType = "dog", 
        [Description("Desired activity duration (short, medium, long)")] string activityDuration = "medium")
    {
        var activityDatabase = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>
        {
            ["sunny"] = new()
            {
                ["dog"] = new()
                {
                    ["short"] = ["Fetch in the yard", "Quick walk around the block", "Backyard agility course"],
                    ["medium"] = ["Dog park visit", "Hiking trail", "Beach walk", "Frisbee in the park"],
                    ["long"] = ["Long hike", "Dog beach day", "Camping trip", "Outdoor training session"]
                },
                ["cat"] = new()
                {
                    ["short"] = ["Supervised patio time", "Window bird watching", "Balcony exploration"],
                    ["medium"] = ["Harness walk in garden", "Outdoor cat enclosure time", "Supervised yard exploration"],
                    ["long"] = ["Extended outdoor enclosure time", "Adventure cat hiking (if trained)"]
                }
            },
            ["cloudy"] = new()
            {
                ["dog"] = new()
                {
                    ["short"] = ["Indoor fetch", "Puzzle toys", "Basic training"],
                    ["medium"] = ["Covered dog park", "Indoor agility", "Mall walk (pet-friendly)"],
                    ["long"] = ["Indoor dog training class", "Dog daycare", "Pet store adventure"]
                },
                ["cat"] = new()
                {
                    ["short"] = ["Interactive toy play", "Treat puzzles", "Laser pointer games"],
                    ["medium"] = ["Cat cafe visit", "Indoor climbing structures", "Hide and seek games"],
                    ["long"] = ["Extended play session", "Cat furniture exploration", "Indoor hunting games"]
                }
            },
            ["rainy"] = new()
            {
                ["dog"] = new()
                {
                    ["short"] = ["Indoor tricks training", "Kong toy stuffing", "Gentle indoor play"],
                    ["medium"] = ["Dog-friendly indoor mall", "Pet store visit", "Indoor dog gym"],
                    ["long"] = ["Dog training class", "Dog spa day", "Extended indoor play session"]
                },
                ["cat"] = new()
                {
                    ["short"] = ["Feather wand play", "Treat dispensing toys", "Cozy nap time"],
                    ["medium"] = ["Interactive puzzle feeders", "Cat TV watching", "Indoor obstacle course"],
                    ["long"] = ["Extended indoor play", "Cat grooming session", "Multi-level cat tree exploration"]
                }
            }
        };

        // Normalize inputs
        var weatherKey = weatherCondition.ToLower() switch
        {
            var w when w.Contains("sun") => "sunny",
            var w when w.Contains("rain") || w.Contains("storm") => "rainy",
            _ => "cloudy"
        };

        var petKey = activityDatabase[weatherKey].ContainsKey(petType.ToLower()) 
            ? petType.ToLower() 
            : "dog";
        
        var durationKey = activityDuration.ToLower() switch
        {
            "short" or "medium" or "long" => activityDuration.ToLower(),
            _ => "medium"
        };

        var activities = activityDatabase[weatherKey][petKey][durationKey];
        var selectedActivities = activities.OrderBy(_ => _random.Next()).Take(3).ToList();

        var activitiesList = string.Join("\n", selectedActivities.Select(a => $"• {a}"));

        return $"""
            Activity Recommendations for your {petType} ({durationKey} duration):

            Weather: {char.ToUpper(weatherCondition[0]) + weatherCondition[1..]}
            Perfect activities:
            {activitiesList}

            Pro Tip: Always bring water and check your pet's paws after outdoor activities!
            """;
    }

    [McpServerTool(Name = "FindPetFriendlyLocations"), Description("Find pet-friendly locations near the specified area")]
    public static string FindPetFriendlyLocations(
        [Description("The city or area to search for pet-friendly locations")] string location, 
        [Description("Type of location (park, restaurant, store, beach, etc.)")] string activityType = "park", 
        [Description("Search radius in miles")] int distanceMiles = 5)
    {
        var locationDatabase = new Dictionary<string, List<LocationData>>
        {
            ["park"] =
            [
                new("Sunset Dog Park", 4.8, ["Off-leash area", "Water fountains", "Agility equipment"], 1.2),
                new("Riverside Trail Park", 4.6, ["Walking trails", "Pet waste stations", "Shaded areas"], 2.3),
                new("Central City Park", 4.4, ["Large open space", "Pet-friendly events", "Parking available"], 3.1),
                new("Meadowbrook Off-Leash Park", 4.9, ["Separate small/large dog areas", "Swimming pond", "Training area"], 4.2)
            ],
            ["restaurant"] =
            [
                new("The Patio Cafe", 4.7, ["Outdoor seating", "Pet menu available", "Water bowls"], 0.8),
                new("Bark & Bistro", 4.5, ["Dog-friendly patio", "Special pet treats", "Pet washing station"], 1.5),
                new("Sunny Side Grill", 4.3, ["Pet-friendly deck", "Shade umbrellas", "Treats for pets"], 2.7),
                new("Garden View Restaurant", 4.6, ["Large patio", "Pet water stations", "Weekend pet events"], 3.4)
            ],
            ["store"] =
            [
                new("Pet Paradise Superstore", 4.8, ["Wide aisles", "Pet grooming", "Training supplies"], 1.1),
                new("Furry Friends Boutique", 4.6, ["Designer pet gear", "Custom accessories", "Photo sessions"], 1.9),
                new("Healthy Paws Pet Store", 4.7, ["Natural foods", "Holistic treatments", "Expert advice"], 2.8),
                new("Adventure Pet Gear", 4.5, ["Outdoor equipment", "Travel accessories", "Expert fitting"], 3.6)
            ],
            ["beach"] =
            [
                new("Sandy Paws Beach", 4.9, ["Off-leash hours", "Fresh water rinse", "Waste bag stations"], 4.8),
                new("Coastal Dog Beach", 4.7, ["Large off-leash area", "Lifeguard on duty", "Pet-friendly parking"], 6.2),
                new("Sunset Cove (Pet Section)", 4.4, ["Designated pet area", "Tide pools", "Beach toys rental"], 7.1)
            ]
        };

        var typeKey = locationDatabase.ContainsKey(activityType.ToLower()) 
            ? activityType.ToLower() 
            : "park";
        
        var availableLocations = locationDatabase[typeKey];
        var filteredLocations = availableLocations
            .Where(loc => loc.Distance <= distanceMiles)
            .OrderByDescending(loc => loc.Rating)
            .Take(3)
            .ToList();

        if (filteredLocations.Count == 0)
        {
            return $"Sorry, I couldn't find any pet-friendly {activityType}s within {distanceMiles} miles of {location}. Try expanding your search radius!";
        }

        var result = $"Top Pet-Friendly {char.ToUpper(activityType[0]) + activityType[1..]}s near {location}:\n\n";

        for (int i = 0; i < filteredLocations.Count; i++)
        {
            var loc = filteredLocations[i];
            result += $"""
                #{i + 1} {loc.Name} - Rating: {loc.Rating}/5
                Distance: {loc.Distance} miles away
                Features: {string.Join(", ", loc.Features)}

                """;
        }

        result += "Pro Tip: Call ahead to confirm current pet policies and hours!";

        return result;
    }

    [McpServerTool(Name = "GetPetCareTips"), Description("Get weather-specific pet care tips and safety advice")]
    public static string GetPetCareTips(
        [Description("Current weather condition")] string weatherCondition, 
        [Description("Type of pet")] string petType = "dog")
    {
        var tipsDatabase = new Dictionary<string, Dictionary<string, List<string>>>
        {
            ["sunny"] = new()
            {
                ["dog"] =
                [
                    "Provide plenty of fresh water and shade",
                    "Walk during cooler hours (early morning/evening)",
                    "Check pavement temperature with your hand - if it's too hot for you, it's too hot for paws",
                    "Consider booties for hot pavement protection",
                    "Watch for signs of overheating: excessive panting, drooling, lethargy"
                ],
                ["cat"] =
                [
                    "Ensure access to cool, shaded areas",
                    "Provide multiple water sources",
                    "Keep indoor cats away from direct sunlight through windows",
                    "Consider cooling mats for comfort",
                    "Monitor for heat stress signs"
                ]
            },
            ["rainy"] = new()
            {
                ["dog"] =
                [
                    "Use waterproof gear if going outside",
                    "Dry thoroughly after being outside to prevent skin issues",
                    "Provide mental stimulation with indoor activities",
                    "Check and clean paws after walks",
                    "Keep a towel by the door for quick cleanups"
                ],
                ["cat"] =
                [
                    "Keep cats indoors during heavy rain",
                    "Provide cozy, dry spots for comfort",
                    "Increase indoor play to compensate for reduced outdoor time",
                    "Monitor humidity levels in the home",
                    "Ensure litter boxes are in dry areas"
                ]
            },
            ["cold"] = new()
            {
                ["dog"] =
                [
                    "Consider warm clothing for short-haired breeds",
                    "Limit time outside for small or elderly dogs",
                    "Protect paws from ice and salt with booties",
                    "Provide warm, dry bedding",
                    "Increase caloric intake if spending time outdoors"
                ],
                ["cat"] =
                [
                    "Provide warm sleeping areas away from drafts",
                    "Keep indoor cats comfortable with adequate heating",
                    "Check outdoor cats for frostbite signs",
                    "Provide extra blankets and warm bedding",
                    "Monitor for signs of cold stress"
                ]
            }
        };

        // Determine weather category
        var weatherKey = weatherCondition.ToLower() switch
        {
            var w when w.Contains("sun") || w.Contains("hot") || w.Contains("warm") => "sunny",
            var w when w.Contains("rain") || w.Contains("storm") || w.Contains("wet") => "rainy",
            _ => "cold"
        };

        var petKey = tipsDatabase[weatherKey].ContainsKey(petType.ToLower()) 
            ? petType.ToLower() 
            : "dog";

        var tips = tipsDatabase[weatherKey][petKey];
        var tipsList = string.Join("\n", tips.Select(t => $"• {t}"));

        return $"""
            Weather Safety Tips for your {char.ToUpper(petType[0]) + petType[1..]}:

            Current conditions: {char.ToUpper(weatherCondition[0]) + weatherCondition[1..]}

            {tipsList}

            Always consult your veterinarian if you notice any concerning symptoms!
            """;
    }

    // Data records
    private record WeatherData(string Condition, int Temp, int Humidity, int Wind, bool PetFriendly);
    private record LocationData(string Name, double Rating, List<string> Features, double Distance);
}
