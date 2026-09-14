#!/usr/bin/env bash

# This script is used to prepare the assets for release.
# Created by Matthew. Maintained by Ocean Apocalypse.
# This is a CI script and is not meant to be used by the end user.

set -e # error out on cd and others
shopt -s nullglob # null globs

mkdir -p "./release-assets"
cd "./all-binaries"

release_dir="$(realpath "../release-assets")"

for dir in */; do
    dir_name="${dir%/}"

    if [[ "$dir_name" == *-win-* ]]; then
        dest_path="$release_dir/$dir_name.zip"
        echo "Windows: $dest_path"
        (
            cd "$dir_name"
            zip -qr "$dest_path" .
        )
    else
        dest_path="$release_dir/$dir_name.tar.gz"
        echo "Linux/macOS: $dest_path"
        tar -czf "$dest_path" -C "$dir_name" .
    fi
done

cd ..
