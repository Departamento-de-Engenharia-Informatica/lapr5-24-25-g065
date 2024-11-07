import * as THREE from 'three';

export function createGround(position = { x: 0, y: 0, z: 0 }) {
    // Carregar a textura da imagem
    const textureLoader = new THREE.TextureLoader();
    const floorTexture = textureLoader.load('textures/floorHospital.jpg'); // Caminho da imagem (ajuste o caminho se necessário)

    // Definir o material com a textura carregada
    const planeMaterial = new THREE.MeshStandardMaterial({
        map: floorTexture, // Aplica a textura
        side: THREE.DoubleSide,
        roughness: 0.5, // Adiciona um pouco de rugosidade ao material
        metalness: 0.1 // Ajuste para um material mais "realista"
    });

    // Criar a geometria e o plano
    const planeGeometry = new THREE.PlaneGeometry(15, 12.5); // Tamanho do plano
    const plane = new THREE.Mesh(planeGeometry, planeMaterial); // Criar o plano
    plane.rotation.x = -Math.PI / 2; // Rodar o plano para que fique horizontal

    // Definir a posição do plano com base nos parâmetros
    plane.position.set(position.x, position.y, position.z);

    return plane;
}
