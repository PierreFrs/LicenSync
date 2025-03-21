// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.19;

contract LicenSync {
    // Mapping of GUIDs (as bytes16) to their corresponding hashes
        mapping(bytes16 => string) private trackHashes;

        // Event to emit when a new track hash is added
        event TrackHashStored(bytes16 indexed guid, string hash);

        // Function to store a new track hash
        function storeTrackHash(bytes16 guid, string memory hash) public {
            trackHashes[guid] = hash;
            emit TrackHashStored(guid, hash);
        }

        // Function to retrieve a track hash by its GUID
        function getTrackHash(bytes16 guid) public view returns (string memory) {
            return trackHashes[guid];
        }
}