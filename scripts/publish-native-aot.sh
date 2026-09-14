#!/usr/bin/env bash

# This script is used to publish a package using Native AOT.
# This script publishes the given package using Native AOT compilation
# aimed towards the given OS, defined by the given flags.

# Created by Matthew. Maintained by Ocean Apocalypse.
# This is an helper script and is not meant to be used by the end user.
# Please note that on Linux, this script also installs the necessary cross-compilation
# tools, as the workflow runs on a x64 machine, while it's necessary to build
# binaries for ARM and ARM64.

set -e

if [[ $# -ne 3 ]]; then
    echo "Usage: $0 <project> <os> <arch>" >&2
    exit 2
fi

project_name=$1
os_name=$2
arch=$3

if [[ -z $project_name || -z $os_name || -z $arch ]]; then
    echo "Project, OS, and architecture must not be empty" >&2
    exit 2
fi

rid="$os_name-$arch"
output_dir="./dist/$project_name-$rid"

dotnet_args=(
    "./src/$project_name/$project_name.csproj"
    -c Release
    -r "$rid"
    -o "$output_dir"
    --self-contained true
)

if [[ $os_name == "linux" ]]; then
    if [[ $arch == "arm" ]]; then
        dotnet_args+=(
            "-p:ObjCopyName=arm-linux-gnueabihf-objcopy"
            "-p:LinkerFlavor=lld"
        )
    elif [[ $arch == "arm64" ]]; then
        dotnet_args+=(
            "-p:ObjCopyName=aarch64-linux-gnu-objcopy"
            "-p:LinkerFlavor=lld"
        )
    fi
fi

dotnet publish "${dotnet_args[@]}"
