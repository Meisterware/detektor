---
name: Proposal / Feature Request
about: Suggest a new feature, capability, or improvement for the Detektor scanner
title: "[Proposal] "
labels: proposal, discussion
---

## Overview

Describe the proposal at a high level.

Proposals may target:

- Detektor CLI capabilities
- scanning and detection features
- OpenPAKT report generation improvements
- CI integration and policy evaluation
- developer tooling or usability improvements
- ecosystem integrations

---

## Motivation

Explain why this proposal is needed.

Consider:

- what problem it solves
- who benefits from it
- how it improves Detektor usability or capabilities
- why this improvement matters now

---

## Proposed Approach

Describe the proposed direction or design.

This does not need to be fully specified, but it should be concrete enough for discussion.

Include, where relevant:

- CLI behaviour or new commands
- scanner detection logic
- OpenPAKT report output changes
- CI or pipeline implications
- compatibility considerations

---

## Alternatives Considered

Describe other approaches that could address the same problem.

---

## Risks and Trade-offs

Describe possible downsides such as:

- increased implementation complexity
- maintenance overhead
- performance impact
- compatibility concerns

---

## Open Questions

List unresolved questions that need discussion.

Examples:

- Should this be implemented in the core CLI or as a plugin/module?
- Should this feature be optional or enabled by default?
- Does this require changes to the OpenPAKT specification?

If the proposal requires changes to the OpenPAKT specification, consider opening a related proposal in the OpenPAKT repository.

---

## Examples

Provide examples, command sketches, or output samples if helpful.

Example:

```
detektor scan ./repo --policy policy.yaml
```

---

## Next Steps

Suggest what should happen next if the proposal gains support.

Examples:

- create a prototype implementation
- add a new CLI command
- add a detection rule
- update documentation
- open a related issue in the OpenPAKT specification repository
