# Pet Planner Workshop (.NET Edition)

Welcome to the PetPlanner Workshop! This guide walks you through building a Pet Planner agent using .NET, Microsoft Agent Framework, and AI Toolkit for VS Code.

## Table of Contents

- [Module 0: Getting Started](#module-0-getting-started)
- [Module 1: Choose a Model](#module-1-choose-a-model)
- [Module 2: Create an Agent](#module-2-create-an-agent)
- [Module 3: Connect an MCP Server](#module-3-connect-an-mcp-server)

---

## Module 0: Getting Started

### Prerequisites

#### Required Accounts

- [Azure](https://signup.azure.com/) subscription
- [GitHub](https://www.github.com) with a [GitHub Copilot](https://github.com/github-copilot/signup) subscription

#### Development Environment

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (or higher)
- [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli-windows?view=azure-cli-latest&pivots=winget) - Used for Azure authentication and resource management
- [Visual Studio Code](https://code.visualstudio.com/download)
  - [AI Toolkit](https://aka.ms/AIToolkit) extension
  - [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) extension
  - [Azure Resources](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azureresourcegroups) extension

### Visual Studio Code Setup

#### Confirm Access to GitHub Models with GitHub Copilot

1. In Visual Studio Code, click the **Toggle Chat** icon to open **GitHub Copilot**.
2. In the **Pick Model** drop-down, confirm the availability of **Claude Sonnet 4** and **Claude Sonnet 4.5**.
3. Set the model to **Claude Sonnet 4.5**.

> **Note**: If you reach your quota limit using **Claude Sonnet 4.5** with GitHub Copilot, feel free to use **Claude Sonnet 4** as an alternative.

#### Confirm Version of the AI Toolkit

1. Open the **Extensions** and select **AI Toolkit**.
2. Confirm that version **0.24.1** or later is installed.
3. If you're using an older version of the AI Toolkit extension, update to the latest version.

#### Create a Microsoft Foundry Project

1. Navigate to the [Microsoft Foundry](https://ai.azure.com?view=foundry) portal.
2. If you do not have any existing Foundry projects, complete the [Microsoft Foundry Quickstart - Create resources](https://learn.microsoft.com/azure/ai-foundry/quickstarts/get-started-code?tabs=azure-ai-foundry&view=foundry#first-run-experience) instructions.
3. If you have an existing Foundry project, deploy the **gpt-4.1-mini** model using the **Default settings**.

#### Sign-In to Azure

1. Open the **Azure Resources** extension (i.e. Azure icon).
2. Select **Sign in to Azure…**.
3. For **The extension 'Azure Resources' wants to sign in using Microsoft**, select **Allow**.
4. For the sign-in screen, enter your Azure subscription credentials.
5. Click **Sign in**.

#### Set the Default Foundry Project in Visual Studio Code

1. In Visual Studio Code, in the **Azure Resources** extension, expand your Azure subscription and expand the **Microsoft Foundry** service.
2. Right click the Foundry project that you created for this workshop and select **Open in Microsoft Foundry Extension**. This sets the project as the default project.
3. Open the **AI Toolkit** extension.
4. Expand **My Resources** and locate your Foundry project (it will show as the default).
5. Confirm that your **gpt-4.1-mini** deployment is listed under the project.

### Documentation

- [AI Toolkit](https://aka.ms/AIToolkit/doc)
- [Microsoft Agent Framework](https://learn.microsoft.com/agent-framework/)
- [Azure AI Evaluation SDK](https://learn.microsoft.com/azure/ai-foundry/how-to/develop/evaluate-sdk?view=foundry)
- [GitHub Copilot](https://code.visualstudio.com/docs/copilot/overview)

---

## Module 1: Choose a Model

The model determines how your agent thinks and responds. You'll choose a model that can understand natural language, fetch data, and generate friendly pet playdate recommendations. Your goal is to select and configure the right model for your Pet Planner agent!

### Instructions

1. In the **AI Toolkit** extension, navigate to **Discover > Model Catalog**.
2. In the **Model Catalog** select the **Hosted by** drop-down and select **Microsoft Foundry**.
3. Search for **gpt-4.1-mini** and click **Try in Playground**.
4. In the **Playground**, confirm that **gpt-4.1-mini** is selected in **Model Preferences**.
5. For the **System prompt**, enter the **Agent System Prompt** provided below.
6. In the chat window, enter the prompt: `It's raining today. What should my dog and I do?`
7. Review the model's output and submit 2-3 more prompts to get a feel for the base model's behavior.

### Agent System Prompt

```
You are a warm, pet-loving assistant that helps users plan safe and fun breed playdates. Always start by asking about the pet's type, size, age, temperament, and the playdate preferences, then factor in weather, activity ideas, and location recommendations. Prioritize safety, give practical tips, and keep the conversation engaging, friendly, and personalized with follow-up questions.
```

### What's Happening

You're testing how the base model responds to pet-related prompts before adding agent logic and tools. This helps you understand the model's natural behavior.

<details>
<summary><strong>Optional: Learn why we chose this model</strong></summary>

Want to understand the trade-offs between different models? Ask GitHub Copilot!

1. Open a new GitHub Copilot chat window via the **Toggle Chat** icon.
2. Click the **Set Mode** drop-down and select **Agent**.
3. Enter this prompt:

```
I want to build a Pet Planner agent. Its job is to help pet owners sniff out the perfect playdate by: (1) checking the weather, (2) fetching fun activity ideas, and (3) pointing to the best spot in town. Which language model(s) would you recommend for this scenario, and why? Explain the trade-offs between models (e.g., reasoning ability, cost, latency, context length) so that I can make an informed choice.
```

GitHub Copilot will invoke the **Get AI Model Guidance** tool from AI Toolkit to provide recommendations.

> **Tip**: If GitHub Copilot doesn't invoke the AI Toolkit tools, enter `#aitk` in the chat window before submitting your prompt.

</details>

### Checkpoint

You should now have tested the gpt-4.1-mini model in the Playground and understand its base behavior.

---

## Module 2: Create an Agent

Agents connect your model with logic and personality. They define how your AI interacts with users — giving it purpose, style, and reasoning. In this step, you'll define the Pet Planner's behavior: how it chats, fetches pet-friendly data, and offers playful suggestions to make every pet outing purr-fect!

### Instructions

1. In the **AI Toolkit** extension, navigate to **Build > Create Agent**.
2. In the **Create Agent** dialog, under **Design an agent without code**, click **Open Agent Builder**.
3. In the **Agent Builder**, for the **Agent Name** enter: `PetPlanner`
3. For the **Model** drop-down, select **gpt-4.1-mini Remote via Microsoft Foundry**.
4. For the **Instructions**, enter the **Agent System Prompt** provided below.
5. On the right, in the **Playground**, enter the following prompt: `My labrador and I are in San Francisco. Recommend something fun to do.`
6. Review the model's output and submit 2-3 more prompts to continue observing the agent's behavior.

### Agent System Prompt

```
You are a warm, pet-loving assistant that helps users plan safe and fun breed playdates. Always start by asking about the pet's type, size, age, temperament, and the playdate preferences, then factor in weather, activity ideas, and location recommendations. Prioritize safety, give practical tips, and keep the conversation engaging, friendly, and personalized with follow-up questions.
```

### What's Happening

You're designing the brain and personality behind your Pet Planner. This is where you decide how it speaks, how it reacts, and what kind of tasks it can handle — from checking the weather to suggesting pet-friendly activities.

### Checkpoint

You should now have an agent that defines how the Pet Planner behaves — ready to interact with your chosen model.

---

## Module 3: Connect an MCP Server

Model Context Protocol (MCP) servers allow agents to fetch live or contextual data securely — such as today's weather or nearby dog parks — so your agent can plan better playdates. Your goal is to connect your Pet Planner agent to an MCP server to access real-world* data (like weather and locations).

> **Note**: We'll be using simulated "live data" for this workshop.

### Part A: Build the MCP Server

1. Open a terminal and navigate to the MCP server project:
   ```bash
   cd DeclarativeAgentsLocal/src
   ```

2. Restore dependencies and build the project:
   ```bash
   dotnet restore
   dotnet build
   ```

3. Verify the build succeeded. The executable will be at:
   ```
   bin/Debug/net8.0/PetPlannerServer.exe
   ```

### Part B: Configure the MCP Server in AI Toolkit

AI Toolkit stores MCP server configurations in a file at `%USERPROFILE%\.aitk\mcp.json` (e.g., `C:\Users\YourUsername\.aitk\mcp.json`).

1. Open the **Pet Planner** agent in **Agent Builder** (navigate to **Build > Create Agent > Open Agent Builder**).

2. In the **Tools** section, click the **+** button and select **MCP Server**.

3. In the dropdown, scroll down and select **Could not find one? Browse more MCP servers**.

4. Scroll to the bottom and select **Command (stdio)**.

5. When prompted for **Command to run**, enter:
   ```
   dotnet run --project c:\labs\FoundryLabs\DeclarativeAgentsLocal\src\PetPlannerServer.csproj
   ```
   > **Note**: Replace the path with your actual project location.

6. When prompted for **Server ID**, enter: `pet-planner`

7. AI Toolkit will open the `mcp.json` file. Verify it contains:
   ```json
   {
     "servers": {
       "pet-planner": {
         "type": "stdio",
         "command": "dotnet",
         "args": [
           "run",
           "--project",
           "c:\\labs\\FoundryLabs\\DeclarativeAgentsLocal\\src\\PetPlannerServer.csproj"
         ]
       }
     }
   }
   ```

8. Save the file.

9. **Reload VS Code**: Press `Ctrl+Shift+P`, type `Reload Window`, and press Enter.

### Part C: Add the MCP Server to Your Agent

1. Return to the **Agent Builder** with your **Pet Planner** agent open.

2. In the **Tools** section, click the **+** button and select **MCP Server**.

3. Select **pet-planner** from the dropdown list.

4. When prompted to **Configure Tools**, select all tools and click **OK**:
   - `GetWeather` - Check weather conditions
   - `GetPetActivities` - Get activity recommendations
   - `FindPetFriendlyLocations` - Find pet-friendly places
   - `GetPetCareTips` - Get weather-specific pet care tips

5. The agent's **Instructions** should be modified to leverage its tools. Next to **Instructions**, click **Improve**.

6. In the **Improve an instruction** window, enter: `include instructions to leverage the MCP tools available to the agent`. Click **Improve**.

7. Review and adjust the improved instructions, or replace them with the **Agent System Prompt** provided below.

### Part D: Test the Agent

1. In the **Playground** (right side of Agent Builder), click the **Clear all messages** icon.

2. Enter the following prompt:
   ```
   My poodle and I are in Los Angeles. What should we do today?
   ```

3. Watch the agent call the MCP server tools:
   - It will check the weather for Los Angeles
   - Recommend activities based on conditions
   - Suggest pet-friendly locations
   - Provide relevant safety tips

4. Respond to any follow-up questions the agent may have.

### Troubleshooting

| Issue | Solution |
|-------|----------|
| Server not appearing in MCP Server dropdown | Reload VS Code (`Ctrl+Shift+P` → `Reload Window`) |
| "Command not found" error | Ensure `dotnet` is in your PATH and the project path is correct |
| Tools not working | Verify the project builds without errors (`dotnet build`) |
| JSON parse error | Check `mcp.json` for syntax errors (use proper escaping for backslashes: `\\`) |

### Agent System Prompt

```
You are a helpful and enthusiastic Pet Planner Assistant.

Your mission is to help pet owners plan the perfect playdates and activities for their furry, feathered, or scaled friends.

CAPABILITIES:

- Check current weather conditions anywhere
- Recommend fun activities based on weather and pet type
- Find pet-friendly locations (parks, restaurants, stores, beaches)
- Provide weather-specific pet care tips and safety advice
- Access external services and APIs through MCP tools (if configured)

PERSONALITY:

- Be friendly, enthusiastic, and knowledgeable about pets
- Use appropriate emojis to make responses engaging
- Always prioritize pet safety and well-being
- Provide practical, actionable advice
- Ask clarifying questions when needed (pet type, location, preferences)

WORKFLOW:

1. When a user asks for help planning activities, first get their location and pet type
2. Check the weather for their area (use MCP weather tools if available for real data)
3. Recommend appropriate activities based on weather conditions
4. Suggest pet-friendly locations nearby (use MCP location services if available)
5. Provide relevant safety tips for the current weather

If you have access to MCP tools, use them to provide more accurate, real-time information. Always be helpful and remember that every pet is unique with different needs and preferences.
```

### What's Happening

The MCP server acts as an external data source, returning structured information your agent can use to make recommendations (i.e., "It's sunny — perfect for a park day!").

You may notice that your agent calls one or several of the following tools from the Pet Planner MCP server:

- GetWeather
- GetPetActivities
- FindPetFriendlyLocations
- GetPetCareTips

### Checkpoint

You should be able to ask the Pet Planner agent "My poodle and I are in Los Angeles. What should we do today?" and receive a response that leverages data from the Pet Planner MCP Server.
