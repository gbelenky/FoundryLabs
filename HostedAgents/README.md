# Microsoft Foundry Hosted Agents

This guide covers creating and deploying **Hosted Agents** using Microsoft Foundry Agent Service. Hosted agents are containerized agentic AI applications that run on Agent Service infrastructure, giving you full programmatic control over agent behavior.

## Table of Contents

- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Understanding Hosted Agents](#understanding-hosted-agents)
- [Quickstart: Create Your First Agent](#quickstart-create-your-first-agent)
- [Create a Hosted Agent](#create-a-hosted-agent)
- [Manage Hosted Agents](#manage-hosted-agents)
- [Observability and Tracing](#observability-and-tracing)
- [Publishing to Channels](#publishing-to-channels)
- [Preview Limits](#preview-limits)
- [References](#references)

## Overview

**Hosted Agents** let you bring your own agent code and run it as a managed containerized service on Microsoft Foundry. Unlike prompt-based declarative agents, hosted agents are built through code and deployed as container images on Microsoft-managed pay-as-you-go infrastructure.

Agent Service handles:
- Provisioning and autoscaling of agents
- Conversation orchestration and state management
- Identity management
- Integration with Foundry tools and models
- Built-in observability and evaluation capabilities
- Enterprise-grade security, compliance, and governance

### Agent Types Comparison

| Aspect | Declarative (Prompt-based) | Hosted (Code-based) |
|--------|---------------------------|---------------------|
| **Definition** | YAML/natural language prompts | Container images with custom code |
| **Tools** | Built-in Foundry tools only | Custom functions + Foundry tools |
| **Deployment** | Save to Foundry | Deploy as managed containers |
| **Flexibility** | Fixed patterns | Full programmatic control |
| **Framework Support** | N/A | Microsoft Agent Framework, LangGraph, custom code |

## Prerequisites

1. **Azure Subscription** with:
   - A [Microsoft Foundry project](https://learn.microsoft.com/en-us/azure/ai-foundry/how-to/create-projects?view=foundry)
   - A deployed model (e.g., `gpt-4o`, `gpt-4.1`)
   - **Azure AI User** role assigned at the project scope

2. **Development Environment**:
   - .NET 8.0 SDK (for C# agents)
   - [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli) authenticated (`az login`)
   - Docker (for local testing and containerization)

3. **Azure Container Registry** (for hosting your container images)

4. **VS Code** with extensions:
   - [Microsoft Foundry](https://marketplace.visualstudio.com/items?itemName=TeamsDevApp.vscode-ai-foundry) - for creating, testing, and deploying hosted agents
   - [AI Toolkit](https://marketplace.visualstudio.com/items?itemName=ms-windows-ai-studio.windows-ai-studio)
   - [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

## Understanding Hosted Agents

### Key Concepts

**Hosted Agents**: Containerized agentic applications that run on Agent Service. They follow a standard lifecycle: create, start, update, stop, and delete.

**Hosting Adapter**: A framework abstraction layer that exposes supported agent frameworks (or custom code) as an HTTP service for local testing and hosted deployments. It provides:
- Simplified local testing on `localhost:8088`
- Automatic protocol translation between Foundry and your framework
- OpenTelemetry observability integration

**Agent Identity**: Unpublished hosted agents run with the project managed identity. Published agents get a distinct agent identity.

### Framework and Language Support

| Framework | .NET | Python |
|-----------|------|--------|
| Microsoft Agent Framework | ✅ | ✅ |
| LangGraph | ❌ | ✅ |
| Custom code | ✅ | ✅ |

### Adapter Packages

- **.NET**: `Azure.AI.AgentServer.Core`, `Azure.AI.AgentServer.AgentFramework`

## Quickstart: Create Your First Agent

1. Open the Command Palette (`Ctrl+Shift+P`)
2. Run: `>Microsoft Foundry: Create new Hosted Agent`
3. Follow the prompts to select language and project location
4. Press `F5` to run and test locally
5. Run `>Microsoft Foundry: Deploy Hosted Agent` to deploy

See [Create a Hosted Agent](#create-a-hosted-agent) for detailed steps.

## Create a Hosted Agent

The [Microsoft Foundry for Visual Studio Code extension](https://marketplace.visualstudio.com/items?itemName=TeamsDevApp.vscode-ai-foundry) provides the simplest way to create, test, and deploy hosted agents.

### Prerequisites

- [Microsoft Foundry extension](https://marketplace.visualstudio.com/items?itemName=TeamsDevApp.vscode-ai-foundry) installed in VS Code
- Project's managed identity with **Azure AI User** and **AcrPull** roles assigned
- A [supported region](https://learn.microsoft.com/en-us/azure/ai-foundry/agents/concepts/hosted-agents?view=foundry) for hosted agents

### Step 1: Create a Hosted Agent Project

1. Open the Command Palette (`Ctrl+Shift+P`)
2. Run: `>Microsoft Foundry: Create a New Hosted Agent`
3. Select your programming language (C# or Python)
4. Select a folder to save your project
5. Enter a name for your workflow project

A new folder is created with all necessary files, including sample code to get started.

### Step 2: Configure Environment

Create or update the `.env` file with your Foundry credentials:

```env
AZURE_AI_PROJECT_ENDPOINT=https://<your-resource-name>.services.ai.azure.com/api/projects/<your-project-name>
AZURE_AI_MODEL_DEPLOYMENT_NAME=<your-model-deployment-name>
```

> **Important**: Add `.env` to your `.gitignore` file.

### Step 3: Run Locally in Interactive Mode

Press `F5` to start debugging, or:

1. Open the Run and Debug view (`Ctrl+Shift+D`)
2. Select **"Debug Local Workflow HTTP Server"** from the dropdown
3. Click the green Start Debugging button

This starts the HTTP server with debugging and opens the AI Toolkit Agent Inspector for interactive testing.

### Step 4: Visualize Workflow Execution

Monitor your hosted agent workflow execution in real time:

1. Open the Command Palette (`Ctrl+Shift+P`)
2. Run: `>Microsoft Foundry: Open Visualizer for Hosted Agents`

A new tab displays the execution graph showing agent interactions.

### Step 5: Deploy the Hosted Agent

1. Open the Command Palette (`Ctrl+Shift+P`)
2. Run: `>Microsoft Foundry: Deploy Hosted Agent`
3. Select your target workspace and configure deployment settings
4. After successful deployment, the agent appears in the **Hosted Agents (Preview)** section of the Foundry extension tree view

You can select the deployed agent to test it using the integrated playground interface.

## Manage Hosted Agents

After deployment, manage your hosted agents directly from the VS Code Foundry extension:

1. Open the **Microsoft Foundry** panel in VS Code
2. Navigate to the **Hosted Agents (Preview)** section in the tree view
3. Right-click on an agent to access management options:
   - **Start** / **Stop** the agent
   - **View logs** and monitor execution
   - **Test** using the integrated playground
   - **Delete** the agent when no longer needed

### Agent Replica Sizes

| CPU (cores) | Memory (GB) |
|-------------|-------------|
| 0.25 | 0.5 |
| 0.5 | 1 |
| 1 | 2 |
| 2 | 4 |
| 3 | 6 |

## Observability and Tracing

### Real-time Visualization in VS Code

Monitor your hosted agent workflow execution in real time:

1. Open the Command Palette (`Ctrl+Shift+P`)
2. Run: `>Microsoft Foundry: Open Visualizer for Hosted Agents`

The visualizer displays the execution graph showing how agents interact and collaborate.

### Tracing with AI Toolkit

1. Install AI Toolkit extension for VS Code
2. Set environment variable `OTEL_EXPORTER_ENDPOINT`
3. Invoke the agent and find traces in AI Toolkit

### Tracing in Foundry Portal

Review traces for your hosted agent on the **Traces** tab in the playground.

## Publishing to Channels

When you publish a hosted agent, Foundry automatically:

1. Creates an agent application resource with a dedicated invocation URL
2. Provisions a distinct agent identity
3. Registers the agent in Microsoft Entra for discovery and governance
4. Enables stable endpoint access

### Available Channels

- **Web application preview** - Demo and test with stakeholders
- **Microsoft 365 Copilot and Teams** - Integration through no-code publishing
- **Stable API endpoint** - Programmatic access via REST API
- **Custom applications** - Embed using SDK integration

> **Important**: After publishing, reconfigure permissions for Azure resources your agent accesses. Project managed identity permissions don't transfer to the new agent identity.

## Preview Limits

| Limit | Value |
|-------|-------|
| Foundry resources with hosted agents per subscription | 100 |
| Maximum hosted agents per Foundry resource | 200 |
| Maximum min_replica count | 2 |
| Maximum max_replica count | 5 |

**Pricing**: Billing for managed hosting runtime is enabled no earlier than April 1, 2026.

## References

- [What are Hosted Agents?](https://learn.microsoft.com/en-us/azure/ai-foundry/agents/concepts/hosted-agents?view=foundry)
- [Foundry Agent Service Overview](https://learn.microsoft.com/en-us/azure/ai-foundry/agents/overview?view=foundry)
- [Quickstart: Create Your First Agent](https://learn.microsoft.com/en-us/azure/ai-foundry/agents/quickstart?view=foundry)
- [Azure.AI.Agents.Persistent SDK Reference](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/ai.agents.persistent-readme)
- [C# Code Samples](https://github.com/microsoft-foundry/foundry-samples/tree/main/samples/csharp/hosted-agents)
- [AI Toolkit Documentation](https://code.visualstudio.com/docs/intelligentapps/overview)
- [Trace Agents with the SDK](https://learn.microsoft.com/en-us/azure/ai-foundry/how-to/develop/trace-agents-sdk?view=foundry)
