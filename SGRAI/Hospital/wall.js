import * as THREE from 'three';

export function createWall(width = 5, height = 3, depth = 1, position = { x: 0, y: 0, z: 0 }, texturePath = null, rotation = { x: 0, y: 0, z: 0 }) {
    const geometry = new THREE.BoxGeometry(width, height, depth);

    let material;

    if (texturePath) {
        const textureLoader = new THREE.TextureLoader();
        const texture = textureLoader.load(texturePath);
        material = new THREE.MeshStandardMaterial({ map: texture });
    } else {
        material = new THREE.MeshBasicMaterial({ color: 0x808080 });
    }

    const wall = new THREE.Mesh(geometry, material);

    wall.position.set(position.x, position.y, position.z);

    wall.rotation.set(rotation.x, rotation.y, rotation.z);

    return wall;
}
