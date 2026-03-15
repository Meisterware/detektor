---
name: Bug Report
about: Report a defect or unexpected behaviour in the Detektor scanner
title: "[Bug] "
labels: bug
---

## Description

Describe the issue clearly and directly.

Examples:

- Detektor CLI fails during a scan
- generated OpenPAKT report is malformed
- CI evaluation produces incorrect pass/fail results
- a command behaves differently than documented
- scanner fails on a specific repository structure

---

## Affected Component

Select the affected component if known:

- CLI command behaviour
- Repository scanning
- Finding detection logic
- OpenPAKT report generation
- CI policy evaluation
- Example artifacts
- Documentation
- Build / CI pipeline

---

## Expected Behaviour

Describe what should happen.

---

## Actual Behaviour

Describe what currently happens.

Include any error messages or incorrect output where possible.

---

## Steps to Reproduce

Provide the smallest possible reproduction steps.

Example:

1. Run `detektor scan <repository>`
2. Generate an OpenPAKT report
3. Observe incorrect findings or CLI error

If possible, provide a minimal repository or configuration that reproduces the issue.

---

## Detektor Version

Example:

```
detektor --version
0.1.0
```

---

## Environment

Provide environment details if relevant.

Examples:

- Operating system
- Shell environment
- CI platform (GitHub Actions, GitLab CI, Azure Pipelines)

---

## Additional Context

Add logs, screenshots, links, or related issues if helpful.

If the issue relates to the **OpenPAKT specification itself**, please consider reporting it in the OpenPAKT specification repository.
