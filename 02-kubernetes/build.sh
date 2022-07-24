#!/bin/bash

docker buildx build \
--platform linux/amd64,linux/arm64 \
--build-arg serviceName=01-kubernetes \
--progress=plain \
--push -t nikitamatsko/otus-hw-01-kubernetes:v1.2 \
. 