import * as THREE from 'three';

export function createGround(position = { x: 0, y: 0, z: 0 }) {
    const textureLoader = new THREE.TextureLoader();
    const floorTexture = textureLoader.load('textures/floorHospital.jpg');

    const planeMaterial = new THREE.MeshStandardMaterial({
        map: floorTexture,
        side: THREE.DoubleSide,
        roughness: 0.5,
        metalness: 0.1
    });

    const planeGeometry = new THREE.PlaneGeometry(15, 12.5);
    const plane = new THREE.Mesh(planeGeometry, planeMaterial);
    plane.rotation.x = -Math.PI / 2;

    plane.position.set(position.x, position.y, position.z);

    return plane;
}
