// models.js
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js'; // Importa o GLTFLoader
import * as THREE from 'three'; // Importa THREE.js

// Função para carregar o modelo GLTF e adicionar à cena
export function loadModel(scene, modelPath, position = { x: 0, y: 0, z: 0 }, scale = { x: 0, y: 0, z: 0 }) {
    const loader = new GLTFLoader(); // Carregador GLTF

    loader.load(modelPath, (gltf) => {
        const model = gltf.scene; // O modelo carregado estará dentro de gltf.scene
        model.position.set(position.x, position.y, position.z); // Define a posição do modelo
        model.scale.set(scale.x, scale.y, scale.z); // Define a escala do modelo
        scene.add(model); // Adiciona o modelo à cena
        console.log('Modelo GLTF carregado com sucesso!');
    }, undefined, (error) => {
        console.error('Erro ao carregar o modelo GLTF', error);
    });
}

export function loadModelWithRotation(scene, modelPath, position = { x: 0, y: 0, z: 0 }, scale = { x: 1, y: 1, z: 1 }, rotation = { x: 0, y: 0, z: 0 }) {
    const loader = new GLTFLoader(); // Carregador GLTF

    loader.load(modelPath, (gltf) => {
        const model = gltf.scene; // O modelo carregado estará dentro de gltf.scene
        model.position.set(position.x, position.y, position.z); // Define a posição do modelo
        model.scale.set(scale.x, scale.y, scale.z); // Define a escala do modelo
        model.rotation.set(rotation.x, rotation.y, rotation.z); // Define a rotação do modelo
        scene.add(model); // Adiciona o modelo à cena
        console.log('Modelo GLTF com rotação carregado com sucesso!');
    }, undefined, (error) => {
        console.error('Erro ao carregar o modelo GLTF com rotação', error);
    });
}