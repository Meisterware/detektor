# Detektor Milestones

## Milestone 1

**v0.1 – Detektor MVP**

Description:

> Initial release of the Detektor CLI providing a minimal AI agent security scanner capable of producing OpenPAKT-compliant reports.

This milestone focuses on building a small, deterministic scanner suitable for CI environments.
The implementation prioritizes a minimal architecture that can scan repositories, execute security scenarios, and produce normalized OpenPAKT findings.

The goal of v0.1 is to demonstrate that a simple security scanner can analyze AI agent artifacts and generate structured findings that CI systems can evaluate automatically.

Issues:
* CLI skeleton and command structure
* Repository scanning engine
* Prompt injection detection rule
* Tool permission validation rule
* OpenPAKT scenario execution
* OpenPAKT report generation
* CI policy evaluation engine

Focus areas:
* Build a minimal .NET CLI scanner architecture
* Implement deterministic repository scanning
* Detect common AI agent risks such as:
  * prompt injection
  * tool permission abuse
* Execute OpenPAKT security scenarios
* Generate OpenPAKT-compliant JSON reports
* Implement CI pass/fail policy evaluation

Outcome:
> A working open-source AI agent security scanner capable of analyzing repositories and producing OpenPAKT-compliant reports that CI pipelines can evaluate deterministically.

The v0.1 release establishes:
* the reference implementation for OpenPAKT scanning
* a minimal rule engine
* a baseline security scenario execution model
* a deterministic CI evaluation pipeline

---

## Milestone 2

**v0.2 – Ecosystem Integration**

Description:

> Detektor v0.2 expands the scanner beyond the minimal MVP by improving interoperability with existing DevSecOps tooling and enabling easier integration into real-world CI environments.

This milestone focuses on making Detektor **practical for production use in CI pipelines**, security dashboards, and DevSecOps workflows.

The goal of v0.2 is to improve integration with common tooling and enable easier configuration of scanner behavior across repositories.

Issues:
* SARIF export support
* CI workflow examples (GitHub Actions, GitLab CI, Azure Pipelines)
* Repository configuration support (`detektor.yaml`)
* Modular rule architecture
* Rule extension interface

Focus areas:
* **SARIF compatibility** for security dashboards and code scanning platforms
* **CI pipeline integration examples**
* Repository-level scanner configuration
* Modular detection rule architecture
* Improved extensibility for custom rules and detectors

Outcome:
> A CI-friendly security scanner that integrates easily with modern DevSecOps tooling and security reporting platforms.

The v0.2 milestone enables:
* security dashboards to visualize Detektor findings
* easier CI adoption across organizations
* extensible rule systems for custom security checks
* standardized scanner configuration per repository

---

## Milestone 3

**v0.3 – Agent Ecosystem Scanning**

Description:

> Detektor v0.3 expands scanning capabilities beyond repository-level analysis to include AI agent configurations, orchestration workflows, and tool ecosystems.

As AI systems increasingly rely on **multiple cooperating agents and connected tools**, security analysis must evaluate interactions across those boundaries.

This milestone introduces deeper inspection of **agent orchestration systems, tool permissions, and multi-agent workflows**.

Issues:
* Agent configuration scanner
* Tool capability validation engine
* Multi-agent workflow analysis
* Cascading failure detection
* Optional tool registry scanning

Focus areas:
* AI **agent configuration security validation**
* **Tool permission and capability analysis**
* **Multi-agent workflow inspection**
* Detection of **cascading failures across agents**
* Optional scanning of **tool registries and agent ecosystems**

Outcome:
> A security scanner capable of analyzing complex AI agent infrastructures and identifying risks across agent orchestration workflows.

The v0.3 milestone enables Detektor to:
* detect security risks in **agent orchestration systems**
* analyze **multi-agent interactions**
* validate **tool permission boundaries**
* detect **cross-agent failure propagation**
