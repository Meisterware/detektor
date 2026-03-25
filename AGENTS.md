# AGENTS.md — Detektor

## Project Overview

Detektor is a **reference CLI scanner** for AI agent security.

It analyzes repositories, configurations, and agent artifacts to detect security issues such as:

- prompt injection
- tool permission misuse
- unsafe agent behavior patterns

Detektor outputs findings in the **OpenPAKT format**, enabling:

- CI policy evaluation
- cross-tool interoperability
- standardized agent security reporting

---

## Relationship to OpenPAKT

Detektor is an **implementation**, not the specification.

- OpenPAKT defines:
  - report schema
  - taxonomy
  - severity model
  - scenario format
  - CI policy semantics

- Detektor:
  - scans inputs
  - produces normalized findings
  - generates OpenPAKT-compliant reports

**Do not redefine OpenPAKT concepts inside Detektor.**

---

## Core Architecture

Detektor follows a strict pipeline:

```

CLI
↓
Artifacts
↓
Rules
↓
Findings
↓
Reporting
↓
Policy

```

### Responsibilities

| Layer | Responsibility |
|------|---------------|
| CLI | command parsing and execution |
| Artifacts | load and normalize inputs |
| Rules | detect security issues |
| Findings | internal normalized representation |
| Reporting | map findings to OpenPAKT |
| Policy | evaluate pass/fail for CI |

---

## Project Structure

### Production Projects

- `Detektor` → main scanner implementation
- `Detektor.OpenPAKT` → OpenPAKT contract models

### Test Project

- `Detektor.Tests`

---

## Folder Structure (Detektor)

```

Detektor/
├─ Cli/
├─ Core/
├─ Artifacts/
├─ Rules/
├─ Findings/
├─ Reporting/
├─ Scenarios/
├─ Policy/
└─ Utils/

```

---

## Namespace Rules

Use **file-scoped namespaces**.

Match namespaces to folders.

### Allowed namespaces

```

Detektor.Cli
Detektor.Core
Detektor.Artifacts
Detektor.Rules
Detektor.Findings
Detektor.Reporting
Detektor.Scenarios
Detektor.Policy
Detektor.Utils

```

Optional sub-namespaces may be used when justified:

```

Detektor.Cli.Commands
Detektor.Core.Pipeline
Detektor.Artifacts.Parsers
Detektor.Rules.Abstractions
Detektor.Reporting.OpenPAKT
Detektor.Policy.Evaluation

```

### OpenPAKT project

```

Detektor.OpenPAKT.Models
Detektor.OpenPAKT.Constants
Detektor.OpenPAKT.Serialization

```

### Naming rules

Use domain-aligned names.

Good:

- `ScanRunner`
- `ArtifactLoader`
- `PromptInjectionRule`
- `ToolPermissionRule`
- `OpenPaktReportWriter`

Avoid vague names:

- `Manager`
- `Helper`
- `Processor`
- `Common`
- `Misc`

---

## Milestone Awareness (CRITICAL)

Detektor evolves in milestones:

- v0.1 – Detektor MVP
- v0.2 – Ecosystem Integration
- v0.3 – Agent Ecosystem Scanning

At any time, the project operates within an **active milestone**.

Agents MUST:

- identify the current milestone (via issues, labels, or instructions)
- implement only what is required for that milestone
- avoid implementing features from future milestones

---

## Scope Control (MANDATORY)

Agents must strictly follow the scope of the **current milestone and issue**.

### General rule

Implement:

- only what is required by the current issue
- only what is necessary for the current milestone

Do NOT implement:

- features from future milestones
- speculative architecture
- unused abstractions

---

### Milestone guidance

#### v0.1 – Detektor MVP

Focus on:

- CLI
- artifact loading
- rule engine
- prompt injection detection
- tool permission validation
- scenario execution
- OpenPAKT report generation
- CI policy evaluation

#### v0.2 – Ecosystem Integration

Adds:

- SARIF mapping
- CI integrations
- detektor.yaml configuration
- compatibility guidance

#### v0.3 – Agent Ecosystem Scanning

Adds:

- multi-agent interaction scenarios
- cross-agent data flow analysis
- distributed findings
- tool invocation trust validation

---

## Non-Goals (Contextual)

Non-goals depend on the current milestone.

Agents must NOT:

- implement features from future milestones
- introduce platform-level architecture prematurely
- add infrastructure not required by the current issue

Examples:

- Do not add SARIF in v0.1
- Do not add plugin systems in v0.1
- Do not implement multi-agent correlation before v0.3

---

## Decision Rule (When Unsure)

If a change:

- is not required by the current issue
- is not required by the current milestone
- introduces additional abstraction

→ DO NOT IMPLEMENT IT

---

## Coding Principles

### 1. Keep it small

Prefer:

- simple classes
- direct logic
- minimal abstraction

Avoid:

- unnecessary interfaces
- deep inheritance
- generic frameworks

---

### 2. Deterministic behavior

Detektor is a **CI tool**.

- same input → same output
- no randomness
- no hidden state
- no unnecessary external dependencies

---

### 3. Pipeline clarity over cleverness

Each stage must be:

- explicit
- testable
- isolated

Do not blur responsibilities between:

- rules
- reporting
- policy

---

### 4. Internal model vs OpenPAKT model

Keep strict separation:

- `Detektor.Findings` → internal model
- `Detektor.OpenPAKT.Models` → output schema

Rules MUST NOT output OpenPAKT directly.

---

### 5. Honest implementation

If a feature is not implemented:

- state it clearly
- do not simulate behavior
- do not fake results

---

## CLI Design Rules

Command shape:

```

detektor scan <target>

```

Rules:

- required target argument
- clean help output
- deterministic exit codes

Exit codes:

- `0` → success
- non-zero → failure

---

## Rule Engine Guidelines

Rules must:

- operate on artifacts
- return findings
- be independent
- be deterministic

Rules must NOT:

- mutate shared/global state
- depend on other rules

---

## Artifact Guidelines

Artifacts represent normalized inputs such as:

- files
- configs
- YAML definitions
- prompt templates

Artifacts should be:

- simple
- structured
- independent of rules

---

## Reporting Guidelines

Reporting layer:

- maps findings → OpenPAKT
- does not perform detection
- does not apply policy

---

## Policy Guidelines

Policy layer:

- evaluates findings
- applies severity thresholds
- returns pass/fail

Must remain deterministic.

---

## Testing Expectations

- test rules independently
- test pipeline behavior
- use fixtures where needed

---

## Dependency Rules

Allowed:

- System.CommandLine
- Spectre.Console (optional, minimal)
- System.Text.Json
- YamlDotNet (only when required)

Avoid adding new dependencies unless necessary.

---

## Contribution Expectations (for Agents)

When modifying code:

- follow folder and namespace conventions
- do not introduce new architectural layers
- do not expand scope beyond the current milestone
- keep changes focused and minimal
- preserve CLI stability

---

## Future Evolution (Awareness Only)

Do NOT implement yet unless required by milestone:

- SARIF support
- detektor.yaml configuration
- rule modularity
- multi-agent correlation

---

## Guiding Principle

Detektor is:

> a minimal, deterministic, CI-first reference scanner for OpenPAKT

Not a full platform.

---

## Final Rule

When unsure:

- choose simplicity
- choose clarity
- choose determinism
- follow the current milestone
