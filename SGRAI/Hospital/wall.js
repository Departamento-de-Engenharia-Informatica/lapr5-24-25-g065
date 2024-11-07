import * as THREE from 'three'; // Importando THREE.js

// Função para criar um paralelopípedo (wall) com textura e rotação
export function createWall(width = 5, height = 3, depth = 1, position = { x: 0, y: 0, z: 0 }, texturePath = null, rotation = { x: 0, y: 0, z: 0 }) {
    const geometry = new THREE.BoxGeometry(width, height, depth); // Geometria do paralelopípedo

    let material;

    // Se uma textura for fornecida, carrega a textura e a aplica
    if (texturePath) {
        const textureLoader = new THREE.TextureLoader();
        const texture = textureLoader.load(texturePath); // Carregar a textura
        material = new THREE.MeshStandardMaterial({ map: texture }); // Usar MeshStandardMaterial para a textura
    } else {
        // Caso não tenha uma textura, usa uma cor padrão
        material = new THREE.MeshBasicMaterial({ color: 0x808080 }); // Cor cinza padrão
    }

    const wall = new THREE.Mesh(geometry, material); // Criar o paralelopípedo com a geometria e material

    // Definir a posição do paralelopípedo
    wall.position.set(position.x, position.y, position.z);

    // Aplicar a rotação lateral (somente ao redor do eixo Z)
    wall.rotation.set(rotation.x, rotation.y, rotation.z);

    return wall; // Retorna o paralelopípedo criado
}
