# Pet Planner Workshop (.NET Edition)

Welcome to the PetPlanner Workshop! This guide walks you through building a Pet Planner agent using .NET, Microsoft Agent Framework, and AI Toolkit for VS Code.

## Table of Contents

- [Module 0: Getting Started](#module-0-getting-started)
- [Module 1: Choose a Model](#module-1-choose-a-model)
- [Module 2: Create an Agent](#module-2-create-an-agent)
- [Module 3: Connect an MCP Server](#module-3-connect-an-mcp-server)
- [Module 4: Generate Agent Code](#module-4-generate-agent-code)
- [Module 5: Trace Agent Responses](#module-5-trace-agent-responses)
- [Module 6: Evaluate Agent Responses](#module-6-evaluate-agent-responses)
- [Summary](#summary)

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
4. Expand **My Resources > Models > Microsoft Foundry**.
5. Confirm that your **gpt-4.1-mini** deployment is listed.

### Documentation

- [AI Toolkit](https://aka.ms/AIToolkit/doc)
- [Microsoft Agent Framework](https://learn.microsoft.com/agent-framework/)
- [Azure AI Evaluation SDK](https://learn.microsoft.com/azure/ai-foundry/how-to/develop/evaluate-sdk?view=foundry)
- [GitHub Copilot](https://code.visualstudio.com/docs/copilot/overview)

---

## Module 1: Choose a Model

The model determines how your agent thinks and responds. You'll choose a model that can understand natural language, fetch data, and generate friendly pet playdate recommendations. Your goal is to select and configure the right model for your Pet Planner agent!

### Instructions

1. Open a new GitHub Copilot chat window via the **Toggle Chat** icon.
2. Click the **Set Mode** drop-down and select **Agent**.
3. Click the **Pick Model** drop-down and select **Claude Sonnet 4.5**.
4. In the chat window, enter the **GitHub Copilot Prompt** provided below and submit.
5. Review the response from GitHub Copilot. Given the non-deterministic nature of language models, responses will vary.
6. If GitHub Copilot requests to open the **Model Catalog**, respond with **Yes** OR click the provided button to access the **Model Catalog**. Alternatively, you can open the **AI Toolkit** extension and navigate to **Model Tools > Model Catalog**.
7. In the **Model Catalog** select the **Hosted by** drop-down and select **GitHub**.
8. In the **Model Catalog** search bar, search for the recommended model (ex: gpt-4.1-mini). Once the model is found, click **Try in Playground**.
9. If prompted to sign-in to GitHub, select **Allow**. For **Select user to authorize** click **Continue** next to the username. Next, for **Visual Studio Code is requesting additional permissions**, select **Authorize Visual-Studio-Code**. After sign-in is complete, select **Open** to open Visual Studio Code.
10. In the **Playground**, in **Model Preferences**, confirm that **OpenAI gpt-4.1-mini (via GitHub)** is selected.
11. For the **System prompt**, enter the **Agent System Prompt** provided below.
12. In the chat window, enter the prompt: `It's raining today. What should my dog and I do?`
13. Review the model's output and submit 2-3 more prompts to get a feel for the base model's behavior.

### GitHub Copilot Prompt

```
I want to build a Pet Planner agent. Its job is to help pet owners sniff out the perfect playdate by: (1) checking the weather, (2) fetching fun activity ideas, and (3) pointing to the best spot in town. Which language model(s) would you recommend for this scenario, and why? Explain the trade-offs between models (e.g., reasoning ability, cost, latency, context length) so that I can make an informed choice.
```

### Agent System Prompt

```
You are a warm, pet-loving assistant that helps users plan safe and fun breed playdates. Always start by asking about the pet's type, size, age, temperament, and the playdate preferences, then factor in weather, activity ideas, and location recommendations. Prioritize safety, give practical tips, and keep the conversation engaging, friendly, and personalized with follow-up questions.
```

### What's Happening

GitHub Copilot calls 1 tool:

- Get AI Model Guidance

> **Note**: If GitHub Copilot doesn't invoke the AI Toolkit tools when generating it's response, you can enter `#aitk` in the chat window to explicitly select which tool(s) you'd like GitHub Copilot to use prior to submitting your prompt.

### Checkpoint

You should now have a model recommendation for your agent and a deployed Microsoft Foundry version of the model.

---

## Module 2: Create an Agent

Agents connect your model with logic and personality. They define how your AI interacts with users — giving it purpose, style, and reasoning. In this step, you'll define the Pet Planner's behavior: how it chats, fetches pet-friendly data, and offers playful suggestions to make every pet outing purr-fect!

### Instructions

1. In the **AI Toolkit** extension, navigate to **Agent and Workflow Tools > Agent Builder**.
2. In the **Agent Builder**, for the **Agent Name** enter: `Pet Planner`
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

1. In VS Code, open the AI Toolkit extension sidebar.

2. Navigate to **MCP Workflow > Browse more MCP Servers**.

3. Select the **Manual** tab.

4. Click **Configure** under **"Command (stdio)"**.

5. When prompted for **Command to run**, enter the full path to run your MCP server:
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

1. In the AI Toolkit extension, navigate to **MCP Workflow > Browse more MCP Servers**.

2. Select the **Configured** tab. You should now see **pet-planner** listed.

3. Open your **Pet Planner** agent in the **Agent Builder** (navigate to **My Resources > Agents** and select it).

4. In the Agent Builder, within the **Tools** section, click the **+** button.

5. Select **MCP Server**.

6. In the **Add MCP Server to Agent** window, select **pet-planner**.

7. When prompted to **Configure Tools**, select all tools and click **OK**:
   - `GetWeather` - Check weather conditions
   - `GetPetActivities` - Get activity recommendations
   - `FindPetFriendlyLocations` - Find pet-friendly places
   - `GetPetCareTips` - Get weather-specific pet care tips

8. The agent's **Instructions** should be modified to leverage its tools. Next to **Instructions**, click **Improve**.

9. In the **Improve an instruction** window, enter: `include instructions to leverage the MCP tools available to the agent`. Click **Improve**.

10. Review and adjust the improved instructions, or replace them with the **Agent System Prompt** provided below.

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
| Server not appearing in Configured tab | Reload VS Code (`Ctrl+Shift+P` → `Reload Window`) |
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

---

## Module 4: Generate Agent Code

You've used the AI Toolkit (AITK) so far to quickly prototype and test your agent's behavior. Now, it's time to move from a low-code prototype to a code-first workflow — giving you full control over your agent's logic, structure, and integration.

Generating agent code allows you to:

- Extend and customize the agent's behavior beyond the AITK UI.
- Add features, APIs, and new data connections directly in code.
- Collaborate through Git and version your agent like any other software project.

> **Warning**: Do not stop the debugger. The debugger should remain running for the rest of this workshop. If the debugger is stopped, the Pet Planner MCP server will no longer run locally which prevents server access for the agent.

### Instructions

1. At the bottom left of the **Agent Builder**, click **View Code**.
2. For the **SDK** select **Microsoft Agent Framework**.
3. For the **Programming Language** select **C#**.
4. Save the file at the root of your project as `PetPlannerAgent.cs`.
5. Before running the script, open a new **terminal** and run the command `az login` to authenticate to Azure. A log-in window will appear. When prompted, select your username and click **Continue**.
6. Next, in the **terminal**, enter the corresponding number for your subscription.
7. Create a new console project and add the required packages:
   ```bash
   dotnet new console -n PetPlannerAgent
   cd PetPlannerAgent
   dotnet add package Azure.AI.Agents.Persistent --prerelease
   dotnet add package Azure.Identity
   ```
8. Copy the generated code into `Program.cs`.
9. In the **terminal** run the command:
   ```bash
   dotnet run
   ```
10. Review the agent output.

### What's Happening

The AI Toolkit's prototype definitions are now being translated into executable code.
This marks a key transition:

- The AI Toolkit was ideal for prototyping — testing logic, tuning behavior, and exploring ideas quickly.
- The code-first workflow empowers you to develop, debug, and extend your agent using standard development practices.

You now have full flexibility to:

- Integrate APIs or additional MCP servers.
- Add new commands and data flows.
- Deploy or share your agent with your team.

### Checkpoint

You should have a Pet Planner agent file (i.e. **PetPlannerAgent**) that runs successfully.

---

## Module 5: Trace Agent Responses

Tracing reveals the decision path your agent takes — which helps debug and improve its reasoning when generating suggestions. Your goal is to enable tracing to understand how your Pet Planner processes information step-by-step.

> **Warning**: Do not stop the debugger. The debugger should remain running for the rest of this workshop. If the debugger is stopped, the Pet Planner MCP server will no longer run locally which prevents server access for the agent.

### Instructions

1. Open the **PetPlannerAgent** project so that GitHub Copilot can use the agent file as context.
2. Open the GitHub Copilot chat window via the **Toggle Chat** icon.
3. Click the **Set Mode** drop-down and select **Agent**.
4. Click the **Pick Model** drop-down and select **Claude Sonnet 4.5**.
5. In the chat window, enter the **GitHub Copilot Prompt** provided below and submit.
6. Review the response from GitHub Copilot. Given the non-deterministic nature of language models, responses will vary.
7. If GitHub Copilot requests to open the **Tracing Viewer**, respond with **Yes** OR click the provided button to access the **Tracing Viewer**. Alternatively, you can open the **AI Toolkit** extension and navigate to **Agent and Workflow Tools > Tracing**. If prompted to allow public and private networks to access this app, select **Allow**.
8. In the **Tracing Viewer** confirm that the **Collector** has started (i.e. blue button under **Tracing**). If the **Collector** has not started, click **Start Collector**.
9. In the **Terminal**, run the command:
   ```bash
   dotnet run
   ```
10. View the traces in the **Tracing Viewer**.
11. If tracing setup is successful, select **Keep** in GitHub Copilot to keep the file changes. Tracing setup is successful if the following conditions are met:
    - There are no errors in the terminal.
    - A trace is logged in the **Tracing Viewer**.
    - A value is provided in the **Tracing Viewer** for **Start Time**, **Duration**, and **Total Tokens**.
    - Trace details include **Chat** (with **Input + Output** and **Metadata**).
    - Trace details include **Execute Tool** with **Metadata**.

### GitHub Copilot Prompt

```
Enable local tracing in my Pet Planner agent using OpenTelemetry for .NET.
```

### What's Happening

Tracing logs the model's chain of reasoning, API calls, and response generation steps — useful for transparency and optimization.

GitHub Copilot calls 1 tool:

- Get Tracing Code Generation Best Practices

### Checkpoint

You can now see a visual/textual trace of your Pet Planner's thought process.

---

## Module 6: Evaluate Agent Responses

Evaluating responses ensures your agent meets expectations — helpful, playful, and reliable — while handling edge cases gracefully. Your goal is to assess your Pet Planner's performance.

> **Warning**: Do not stop the debugger. The debugger should remain running for the rest of this workshop. If the debugger is stopped, the Pet Planner MCP server will no longer run locally which prevents server access for the agent.

### Instructions

1. Open the **PetPlannerAgent** project so that GitHub Copilot can use the agent file as context.
2. Open the GitHub Copilot chat window via the **Toggle Chat** icon.
3. Click the **Set Mode** drop-down and select **Agent**.
4. Click the **Pick Model** drop-down and select **Claude Sonnet 4.5**.
5. In the chat window, enter the **GitHub Copilot Prompt** provided below and submit.
6. Review the response from GitHub Copilot. Given the non-deterministic nature of language models, responses will vary.
7. If GitHub Copilot requests that you confirm the recommended evaluators for evaluation, respond: `Only relevance and coherence`.
8. If GitHub Copilot inquires whether to create a dataset with queries, respond: `Yes, only create 3 rows of data.`.
9. If GitHub Copilot inquires whether to create a dataset with responses, respond: `Yes, collect responses.`.
10. You may be prompted to allow GitHub Copilot to run commands. If prompted, select **Allow**. GitHub Copilot may occasionally run a command that results in an error. If that occurs, GitHub Copilot will likely resolve the error on its own.
11. After GitHub Copilot completes its task of creating a test dataset, you'll be prompted to confirm the evaluation plan. Review and either respond `yes` or respond with your requested changes.
12. While GitHub Copilot creates the evaluation file, you may also be prompted to allow GitHub Copilot to install any required dependencies. As a precaution, review the request before selecting **Allow**.
13. After GitHub Copilot creates the evaluation file, you may be prompted to allow GitHub Copilot to run the evaluation file. If you'd prefer to run the file yourself, select **Skip**, otherwise select **Allow**.
14. Review the evaluation results in the terminal. Alternatively, open the evaluation results JSON file (e.g. `evaluation_results.json` although yours may be named differently) and open the **Command Palette** (CTRL+SHIFT+P). Type `Format Document` to format the JSON file for a better view of the file's content. As a bonus, you can prompt GitHub Copilot to `create a markdown report of the evaluation results`.

### GitHub Copilot Prompt

```
Add evaluation to my agent using Azure AI Evaluation SDK for .NET.
```

### What's Happening

Copilot compares your agent's responses against best practices and performance criteria, surfacing improvements in tone, relevance, or correctness.

GitHub Copilot calls these tools:

- Evaluation Planner
- Get Evaluation Agent Runner Best Practices
- Get Evaluation Code Generation Best Practices
- Get AI Model Guidance

### Troubleshooting

If the evaluation file created by GitHub Copilot produces an error, try resolving by referencing the [Azure AI Evaluation Local Evaluation](https://learn.microsoft.com/azure/ai-foundry/how-to/develop/evaluate-sdk?view=foundry#conversation-support-for-text) documentation. Provided below are a few common things to check:

- Confirm that GitHub Copilot uses the **Azure AI Evaluation SDK**.
- Confirm that the `model_config` is properly configured with your Microsoft Foundry endpoint and deployment name.
- Confirm that the `azure-deployment` value within the `model_config` is `gpt-4.1-mini`.
- Confirm that the `azure_endpoint` value within the `model_config` is the same as the **Target URI** for your model deployment.

### Checkpoint

You now have synthetic data ready to evaluate your Pet Planner's responses. You should also have an evaluation script that runs the recommended evaluators, and results from your latest evaluation run.

---

## Summary

You've completed the **Pet Planner Workshop (.NET Edition)** — creating a smart, friendly agent that plans pet playdates using data and AI reasoning.

Along the way, you learned how to design, configure, generate, trace, and evaluate an AI-powered workflow.

### What You Built

| Module | Description |
|--------|-------------|
| **Module 1** | Chose a model optimized for conversational AI |
| **Module 2** | Created an agent with personality and instructions |
| **Module 3** | Connected to an MCP server for live data |
| **Module 4** | Generated C# code using Microsoft Agent Framework |
| **Module 5** | Enabled tracing to visualize agent reasoning |
| **Module 6** | Evaluated agent responses for quality |

### Next Steps

Continue exploring the tools and technologies you used in this workshop:

| Topic | Description | Link |
|-------|-------------|------|
| **AI Toolkit for VS Code** | Build, test, and manage AI agents directly in your editor | [AI Toolkit](https://aka.ms/AIToolkit) |
| **GitHub Copilot Chat** | Learn how to prompt and collaborate effectively with Copilot in VS Code | [Copilot Chat Docs](https://docs.github.com/copilot) |
| **Model Context Protocol (MCP)** | Understand how agents access live data securely | [MCP Overview](https://aka.ms/mcp-for-beginners) |
| **Agent Evaluation & Tracing** | Learn advanced debugging and evaluation workflows | [Observability in Generative AI](https://learn.microsoft.com/azure/ai-foundry/concepts/observability?view=foundry) |
| **Microsoft Agent Framework** | Build production agents with .NET | [Agent Framework Docs](https://learn.microsoft.com/agent-framework/) |

---

## Project Structure

```
DeclarativeAgentsLocal/
├── README.md                    # This workshop guide
└── src/
    └── PetPlannerServer.cs      # MCP server implementation for .NET
```
