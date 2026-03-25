# Detektor

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)
[![OpenPAKT](https://img.shields.io/badge/OpenPAKT-v0.1-green)](https://github.com/Meisterware/openpakt-spec)
[![Status](https://img.shields.io/badge/status-early%20development-orange)]()
[![Issues](https://img.shields.io/github/issues/meisterware/detektor)](https://github.com/Meisterware/detektor/issues)
[![PRs](https://img.shields.io/github/issues-pr/meisterware/detektor)](https://github.com/Meisterware/detektor/pulls)

**Detektor** is a lightweight **AI agent security scanner** designed for **CI pipelines**.

It analyses agent prompts, configurations, workflows, and related artifacts to detect security risks and produce **OpenPAKT-compliant reports**.

Detektor helps teams identify issues such as:

* prompt injection vulnerabilities
* unsafe tool permissions
* other AI agent security weaknesses

The scanner generates structured findings that can be evaluated by CI systems using the **OpenPAKT CI Policy Evaluation Semantics**.

---

## Project Goals

Detektor is designed to be:

* **minimal** — small and easy to understand
* **deterministic** — predictable results suitable for CI gating
* **implementation-agnostic** — works with different agent frameworks
* **DevSecOps-friendly** — integrates easily into build pipelines

The project focuses on **practical security scanning for AI agents** rather than full runtime protection.

---

## Project Status

Detektor is currently in **early development**.

The initial milestone focuses on implementing the **OpenPAKT v0.1 core specification**, including:

* report schema support
* basic rule detection
* example security scenarios
* CI policy evaluation compatibility

---

## Example

Run a repository scan:

```text
detektor scan .
```

Generate an OpenPAKT report:

```text
detektor report --output report.json
```

---

## Relationship to OpenPAKT

Detektor is the **reference scanner implementation** for the **OpenPAKT specification**.

OpenPAKT defines a standardised way to represent:

* AI agent security findings
* reproducible security testing scenarios
* CI policy evaluation semantics

Detektor implements these concepts and produces reports conforming to the OpenPAKT report schema.

Example report structure:

```json
{
  "schema_version": "0.1",
  "scan": {
    "tool": {
      "name": "detektor",
      "version": "0.1.0"
    },
    "timestamp": "2026-03-08T10:00:00Z"
  },
  "target": {
    "type": "repository",
    "path": "."
  },
  "summary": {
    "critical": 0,
    "high": 1,
    "medium": 0,
    "low": 0,
    "informational": 0
  }
}
```

The OpenPAKT report format provides a **stable contract for CI pipelines and security dashboards**. 

---

## Roadmap

Detektor evolves alongside the **OpenPAKT specification**, expanding scanning capabilities in stages while keeping the core architecture minimal and extensible.

### v0.1 — Detektor MVP (🚧 In-Progress)

Initial release providing a minimal security scanner capable of producing OpenPAKT-compliant reports.

Key capabilities:

- repository scanning
- prompt injection detection
- tool permission validation
- OpenPAKT scenario execution
- OpenPAKT report generation
- CI policy evaluation

The goal is to provide a small, deterministic scanner suitable for CI integration. 

---

### v0.2 — Ecosystem Integration

Detektor v0.2 expands the scanner beyond the minimal MVP by improving interoperability with existing DevSecOps tooling and enabling easier integration into real-world CI pipelines and security workflows.

Planned improvements include:
- SARIF export for security dashboards and code scanning platforms
- CI workflow examples for GitHub Actions, GitLab CI, and Azure Pipelines
- Repository configuration support via a detektor.yaml file
- Modular rule architecture for extending detection capabilities

The goal of v0.2 is to make Detektor practical for real-world CI environments.

---

### v0.3 — Agent Ecosystem Scanning

Detektor v0.3 expands scanning capabilities beyond repository-level analysis to include AI agent configurations, orchestration workflows, and tool ecosystems.

Planned capabilities include:
- Agent configuration scanning
- Tool capability validation
- Multi-agent workflow analysis
- Detection of cascading failures across agent workflows
- Optional scanning of tool registries and agent ecosystems

The goal of v0.3 is to evolve Detektor into a security scanner for AI agent infrastructures and orchestration systems.

---

## Vision

To support a **growing ecosystem of AI agent security tooling**.

Detektor aims to help developers and organisations identify and mitigate risks in AI agent systems before deployment.

---

## Organisation

Detektor is developed by **Meisterware** as part of the broader effort to improve **AI agent security tooling and standards**.
