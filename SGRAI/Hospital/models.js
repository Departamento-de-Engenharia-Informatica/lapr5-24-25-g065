import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader.js';
import * as THREE from 'three';

export function loadModel(scene, modelPath, position = { x: 0, y: 0, z: 0 }, scale = { x: 0, y: 0, z: 0 }) {
    const loader = new GLTFLoader();

    loader.load(modelPath, (gltf) => {
        const model = gltf.scene;
        model.position.set(position.x, position.y, position.z);
        model.scale.set(scale.x, scale.y, scale.z);
        scene.add(model);
        console.log('Modelo GLTF carregado com sucesso!');
    }, undefined, (error) => {
        console.error('Erro ao carregar o modelo GLTF', error);
    });
}

export function loadModelWithRotation(scene, modelPath, position = { x: 0, y: 0, z: 0 }, scale = { x: 1, y: 1, z: 1 }, rotation = { x: 0, y: 0, z: 0 }) {
    const loader = new GLTFLoader();

    loader.load(modelPath, (gltf) => {
        const model = gltf.scene;
        model.position.set(position.x, position.y, position.z);
        model.scale.set(scale.x, scale.y, scale.z);
        model.rotation.set(rotation.x, rotation.y, rotation.z);
        scene.add(model);
        console.log('Modelo GLTF com rotação carregado com sucesso!');
    }, undefined, (error) => {
        console.error('Erro ao carregar o modelo GLTF com rotação', error);
    });
}