#!/usr/bin/env bash

# This script is used from within a workflow to set the SEMVER environment
# variable used to decide which version is to be analyzed and released.
# This script extracts and sets the SEMVAR environment variable from the shared
# project properties, if the workflow was dispatched manually, or from the tag name
# if dispatched via a tag push.

# Created by Matthew. Maintained by Ocean Apocalypse.
# This is a CI script and is not meant to be used by the end user.

set -euo pipefail

if [[ $# -ne 2 ]]; then
    echo "Usage: $0 <ref-type> <ref-name>" >&2
    exit 2
fi

ref_type=$1
ref_name=$2

if [[ $ref_type == tag ]]; then
    if [[ ${#ref_name} -lt 6 ]]; then
        echo "Reference name must have at least 6 characters" >&2
        exit 2
    fi

    semver=${ref_name:1}
else
    props_file="./Directory.Build.props"
    version_prefix="$(
        xmllint --xpath 'string(//VersionPrefix)' "$props_file"
    )"
    version_suffix="$(
        xmllint --xpath 'string(//VersionSuffix)' "$props_file"
    )"

    if [[ -n $version_suffix ]]; then
        semver="${version_prefix}${version_suffix}"
    else
        semver="$version_prefix"
    fi
fi

printf 'semver=%s\n' "$semver" >> "$GITHUB_OUTPUT"
