# Microsoft Foundry Workflows

## Overview

Microsoft Foundry Workflows enables the creation, orchestration, and deployment of AI agent workflows within the Microsoft Foundry platform. This repository contains workflow definitions and configurations for building intelligent, multi-step AI applications.

## Features

- **Declarative Workflow Definitions**: Define workflows using YAML or JSON configurations
- **Agent Orchestration**: Coordinate multiple AI agents to accomplish complex tasks
- **Built-in Connectors**: Integrate with Azure services, APIs, and external data sources
- **Observability**: Built-in tracing, monitoring, and debugging capabilities
- **Scalable Execution**: Run workflows at scale with Azure infrastructure

## Workflow Concepts

Workflows are UI-based tools in Microsoft Foundry that enable you to create declarative, predefined sequences of actions orchestrating agents and business logic in a visual builder. They allow you to build intelligent automation systems that seamlessly blend AI agents with business processes.

### When to Use Workflows

Workflows are ideal for scenarios where you need to:

- **Orchestrate multiple agents** in a repeatable process
- **Add branching logic** (if/else) and variable handling without writing code
- **Create human-in-the-loop steps** such as approvals or clarifying questions

### Workflow Patterns

Foundry provides templates for common orchestration patterns:

| Pattern | Description | Use Cases |
|---------|-------------|-----------|
| **Human in the Loop** | Asks the user a question and awaits user input to proceed | Approval requests, obtaining information from users |
| **Sequential** | Passes the result from one agent to the next in a defined order | Step-by-step workflows, pipelines, multi-stage processing |
| **Group Chat** | Dynamically passes control between agents based on context or rules | Dynamic workflows, escalation, fallback, expert handoff |

### Nodes

Nodes are the building blocks of your workflow. Each node performs a specific action in sequence.

**Common node types include:**

- **Agent**: Invoke an agent
- **Logic**: Use if/else, go to, or for each
- **Data Transformation**: Set a variable or parse a value
- **Basic Chat**: Send a message or ask a question to an agent

### Agents in Workflows

You can add any Foundry agent from your project to the workflow. Agent nodes allow you to:

- Add existing agents from your Foundry project
- Create new agents with customized capabilities
- Configure model, prompt, and tools for each agent
- Set up structured JSON output with schemas

### Power Fx Expressions

Power Fx is a low-code language using Excel-like formulas for creating complex logic. Use Power Fx to:

- Set variable values
- Parse strings
- Evaluate conditions
- Manipulate data

**Variable Scoping:**
- System variables: Use `System.` prefix (e.g., `System.LastMessage.Text`)
- Local variables: Use `Local.` prefix (e.g., `Local.Var01`)

**Available System Variables:**
- `Conversation.Id` - Unique ID of the current conversation
- `Conversation.LocalTimeZone` - User's time zone
- `LastMessage.Text` - Previous message sent by the user
- `User.Language` - User language locale per conversation

### Additional Features

- **YAML Visualizer View**: Store and edit workflows as YAML files with full version history
- **Versioning**: Each save creates a new, immutable version with complete history
- **Notes**: Add annotations to the workflow visualizer for extra context

### Development Options

- **Declarative (Low-code)**: Work with workflow YAML in Visual Studio Code
- **Hosted (Pro-code)**: Build workflows programmatically with full code control

## References

- [Workflow Concepts - Azure AI Foundry](https://learn.microsoft.com/en-us/azure/ai-foundry/agents/concepts/workflow?view=foundry)
- [Microsoft Agent Framework Workflow Orchestrations](https://learn.microsoft.com/en-us/agent-framework/user-guide/workflows/orchestrations/overview)
- [Power Fx Formula Reference](https://learn.microsoft.com/en-us/power-platform/power-fx/formula-reference-copilot-studio)
- [Youtube content on Workflows](https://www.youtube.com/watch?v=AqM5WLq2VtY&list=PLlrxD0HtieHj61bBwrAqd5yHvwjB8s_oz&index=7)
