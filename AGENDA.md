# Microsoft Foundry Labs — 1-Day Training Agenda

## Overview

A hands-on, full-day workshop for building AI agents and workflows using VS Code AI Toolkit and Microsoft Foundry. Participants will progress from no-code declarative agents to pro-code hosted agents and multi-agent workflows.

**Duration:** 8 hours (9:00 AM – 5:00 PM)

---

## Morning

### 9:00 – 9:30 | Welcome & Environment Setup (30 min)

- Introductions and training objectives
- Verify prerequisites:
  - VS Code with AI Toolkit, C# Dev Kit, Azure Resources extensions
  - .NET 8.0 SDK, Azure CLI
  - Azure subscription with AI Services resource
- Sign in to Azure and configure the default Foundry project
- Confirm access to GitHub Models via GitHub Copilot

### 9:30 – 11:00 | Lab 1: Declarative Agents — Local / Pet Planner Workshop (90 min)

> *.NET edition — build agents locally with MCP servers*

| Time | Topic |
|------|-------|
| 9:30 – 9:45 | **Module 1** — Choose a model: compare trade-offs, test in Playground |
| 9:45 – 10:00 | **Module 2** — Create an agent: define persona, instructions, test behavior |
| 10:00 – 10:30 | **Module 3** — Connect an MCP server: build, configure, and test the Pet Planner MCP server |
| 10:30 – 10:45 | **Module 4** — Generate agent code: export to C# with Microsoft Agent Framework |
| 10:45 – 11:00 | **Module 5** — Trace agent responses: enable OpenTelemetry tracing, inspect in Tracing Viewer |

### 11:00 – 11:15 | Break (15 min)

### 11:15 – 11:45 | Lab 1 continued: Pet Planner Evaluation (30 min)

| Time | Topic |
|------|-------|
| 11:15 – 11:45 | **Module 6** — Evaluate agent responses: Azure AI Evaluation SDK, create dataset, run evaluators, analyze results |

### 11:45 – 1:00 | Lab 2: Declarative Agents — Remote (75 min)

> *No-code agents with Agent Builder and Foundry tools*

| Time | Topic |
|------|-------|
| 11:45 – 12:00 | Enable local authentication, create agent in Agent Builder |
| 12:00 – 12:15 | Code Interpreter — upload GDP dataset, generate visualizations |
| 12:15 – 12:30 | Web Search — real-time information queries |
| 12:30 – 12:45 | File Search — upload study sessions data, RAG queries |
| 12:45 – 1:00 | Multi-tool instructions, evaluation & tracing in Foundry portal |

---

## 1:00 – 2:00 | Lunch Break (60 min)

---

## Afternoon

### 2:00 – 3:15 | Lab 3: Hosted Agents (75 min)

> *Containerized pro-code agents with Foundry Agent Service*

| Time | Topic |
|------|-------|
| 2:00 – 2:15 | Overview of hosted vs. declarative agents, framework support |
| 2:15 – 2:45 | Create a hosted agent: scaffold project, configure hosting adapter, test locally |
| 2:45 – 3:00 | Deploy to Foundry Agent Service: containerize, push to ACR, deploy |
| 3:00 – 3:15 | Observability, tracing, and publishing to channels |

### 3:15 – 3:30 | Break (15 min)

### 3:30 – 4:30 | Lab 4: Workflows (60 min)

> *Multi-agent workflow orchestration*

| Time | Topic |
|------|-------|
| 3:30 – 3:45 | Workflow concepts: nodes, agents, patterns (Sequential, Group Chat, Human-in-the-Loop) |
| 3:45 – 4:05 | Build a workflow: add agent nodes, configure branching logic, use Power Fx expressions |
| 4:05 – 4:20 | Test, version, and deploy workflows |
| 4:20 – 4:30 | YAML visualizer, declarative vs. pro-code workflow options |

### 4:30 – 5:00 | Wrap-Up & Q&A (30 min)

- Recap of all four labs
- Key takeaways: when to use declarative vs. hosted agents vs. workflows
- Next steps and additional resources
- Open Q&A

---

## Summary

| Block | Duration | Lab |
|-------|----------|-----|
| Environment Setup | 30 min | — |
| Pet Planner Workshop (.NET) | 120 min | Lab 1 |
| Declarative Agents Remote | 75 min | Lab 2 |
| Hosted Agents | 75 min | Lab 3 |
| Workflows | 60 min | Lab 4 |
| Wrap-Up & Q&A | 30 min | — |
| Breaks + Lunch | 90 min | — |
| **Total** | **8 hours** | |
