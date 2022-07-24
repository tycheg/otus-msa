#!/bin/bash

docker buildx build \
--platform linux/amd64,linux/arm64 \
--build-arg serviceName=02-kubernetes \
--progress=plain \
--push -t nikitamatsko/otus-hw-02-kubernetes:v1.1 \
. 