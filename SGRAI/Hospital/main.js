import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js'; // Importando o OrbitControls
import { loadModel } from './models.js'; // Importando a função loadModel
import { createWall } from './wall.js'; // Importando a função createWall
import { createGround } from './ground.js'; // Importando a função createGround
import { loadModelWithRotation } from './models.js';

// Inicializar cena, câmera e renderizador
const scene = new THREE.Scene();
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// Adicionar o plano à cena usando a função createGround com posição personalizada
const plane = createGround({ x: 10, y: 0, z: 0 });
scene.add(plane);
const plane1 = createGround({ x: 10, y: 0, z: 10 });
scene.add(plane1);
const plane2 = createGround({ x: 0, y: 0, z: 0 });
scene.add(plane2);
const plane3 = createGround({ x: 0, y: 0, z: 10 });
scene.add(plane3);

// Carregar o modelo GLTF (hospital_bed.glb) usando a função loadModel do models.js
loadModel(scene, 'models/hospital_bed.glb', { x: -5, y: 0, z: -5 }, { x: 2, y: 2, z: 2 });
loadModel(scene, 'models/hospital_bed.glb', { x: 2.5, y: 0, z: -5 }, { x: 2, y: 2, z: 2 });
loadModel(scene, 'models/hospital_bed.glb', { x: 8, y: 0, z: -5 }, { x: 2, y: 2, z: 2 });
loadModel(scene, 'models/hospital_bed.glb', { x: 15, y: 0, z: -5 }, { x: 2, y: 2, z: 2 });

loadModel(scene, 'models/hospital_bed.glb', { x: -5, y: 0, z: 15 }, { x: 2, y: 2, z: 2 });
loadModel(scene, 'models/hospital_bed.glb', { x: 2, y: 0, z: 15 }, { x: 2, y: 2, z: 2 });
loadModel(scene, 'models/hospital_bed.glb', { x: 8, y: 0, z: 15 }, { x: 2, y: 2, z: 2 });
loadModel(scene, 'models/hospital_bed.glb', { x: 15, y: 0, z: 15 }, { x: 2, y: 2, z: 2 });

loadModel(scene, 'models/hospital_door.glb', { x: -1.3 , y: 0, z: 2  }, { x: 1.2, y: 1.2, z: 1.2});
loadModel(scene, 'models/hospital_door.glb', { x: 1.3 , y: 0, z: 2  }, { x: 1.2, y: 1.2, z: 1.2});

loadModel(scene, 'models/hospital_door.glb', { x: 8.75 , y: 0, z: 2  }, { x: 1.2, y: 1.2, z: 1.2});
loadModel(scene, 'models/hospital_door.glb', { x: 11.25 , y: 0, z: 2  }, { x: 1.2, y: 1.2, z: 1.2});

loadModel(scene, 'models/hospital_door.glb', { x: -1.3 , y: 0, z: 8  }, { x: 1.2, y: 1.2, z: 1.2});
loadModel(scene, 'models/hospital_door.glb', { x: 1.3 , y: 0, z: 8  }, { x: 1.2, y: 1.2, z: 1.2});

loadModel(scene, 'models/hospital_door.glb', { x: 8.75 , y: 0, z: 8  }, { x: 1.2, y: 1.2, z: 1.2});
loadModel(scene, 'models/hospital_door.glb', { x: 11.25 , y: 0, z: 8  }, { x: 1.2, y: 1.2, z: 1.2});

loadModelWithRotation(scene, 'models/human_body.glb', { x: -3, y: 2, z: -5 }, { x: 0.7, y: 0.7, z: 0.7 }, { x: -Math.PI / 2, y:0, z:  Math.PI / 2 });
loadModelWithRotation(scene, 'models/human_body.glb', { x: 4, y: 2, z: -5 }, { x: 0.7, y: 0.7, z: 0.7 }, { x: -Math.PI / 2, y:0, z:  Math.PI / 2 });
loadModelWithRotation(scene, 'models/human_body.glb', { x: 9.5, y: 2, z: -5 }, { x: 0.7, y: 0.7, z: 0.7 }, { x: -Math.PI / 2, y:0, z:  Math.PI / 2 });
loadModelWithRotation(scene, 'models/human_body.glb', { x: 16.5, y: 2, z: -5 }, { x: 0.7, y: 0.7, z: 0.7 }, { x: -Math.PI / 2, y:0, z:  Math.PI / 2 });

loadModelWithRotation(scene, 'models/human_body.glb', { x: -3, y: 2, z: 15 }, { x: 0.7, y: 0.7, z: 0.7 }, { x: -Math.PI / 2, y:0, z:  Math.PI / 2 });
loadModelWithRotation(scene, 'models/human_body.glb', { x: 4, y: 2, z: 15 }, { x: 0.7, y: 0.7, z: 0.7 }, { x: -Math.PI / 2, y:0, z:  Math.PI / 2 });
loadModelWithRotation(scene, 'models/human_body.glb', { x: 9.5, y: 2, z: 15 }, { x: 0.7, y: 0.7, z: 0.7 }, { x: -Math.PI / 2, y:0, z:  Math.PI / 2 });
loadModelWithRotation(scene, 'models/human_body.glb', { x: 16.5, y: 2, z: 15 }, { x: 0.7, y: 0.7, z: 0.7 }, { x: -Math.PI / 2, y:0, z:  Math.PI / 2 });

// Criar um paralelopípedo (parede) usando a função createWall

const wall = createWall(15, 5, 0.5, { x: 0, y: 2.5, z: -6 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wall);
const wall1 = createWall(15, 5, 0.5, { x: 10, y: 2.5, z: -6 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wall1);
const wallPorta = createWall(3, 5, 0.3, { x: -7.25, y: 2.5, z: 5 }, 'textures/door1.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallPorta);
const wall2 = createWall(9.5, 5, 0.5, { x: -7.25, y: 2.5, z: -1.25 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wall2);
const wall3 = createWall(9.5, 5, 0.5, { x: -7.25, y: 2.5, z: 11.25 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wall3);
const wall4 = createWall(15, 5, 0.5, { x: 0, y: 2.5, z: 16 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wall4);
const wall5 = createWall(15, 5, 0.5, { x: 10, y: 2.5, z: 16 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wall5);
const wall6 = createWall(11, 5, 0.5, { x: 17.25, y: 2.5, z: -0.5 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wall6);
const wall7 = createWall(11.5, 5, 0.5, { x: 17.25, y: 2.5, z: 10 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wall7);

const wallInterior1 = createWall(5, 5, 0.2, { x: -5, y: 2.5, z: 2 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior1);
const wallInterior2 = createWall(5, 5, 0.2, { x: 5, y: 2.5, z: 2 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior2);

const wallInterior3 = createWall(8, 5, 0.2, { x: 0, y: 2.5, z: -2 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior3);
const wallInterior4 = createWall(5, 5, 0.5, { x: -7.25, y: 2.5, z: 11.25 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior4);

const wallInterior5 = createWall(5, 5, 0.2, { x: 15, y: 2.5, z: 2 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior5);
const wallInterior6 = createWall(8, 5, 0.2, { x: 10, y: 2.5, z: -2 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior6);
const wallInterior7 = createWall(8, 5, 0.2, { x: 5, y: 2.5, z: -2 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior7);

const wallInterior8 = createWall(8, 5, 0.2, { x: 0, y: 2.5, z: 12 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior8);
const wallInterior9 = createWall(8, 5, 0.2, { x: 10, y: 2.5, z: 12 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior9);
const wallInterior10 = createWall(8, 5, 0.2, { x: 5, y: 2.5, z: 12 }, 'textures/wall.jpg', { x: 0, y: Math.PI / 2, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior10);

const wallInterior11 = createWall(5, 5, 0.2, { x: 15, y: 2.5, z: 8 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior11);
const wallInterior12 = createWall(5, 5, 0.2, { x: 5, y: 2.5, z: 8 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior12);
const wallInterior13 = createWall(5, 5, 0.2, { x: -5, y: 2.5, z: 8 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior13);


const wallInterior14 = createWall(5, 1, 0.2, { x: 0, y: 4.5, z: 8 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior14);
const wallInterior15 = createWall(5, 1, 0.2, { x: 10, y: 4.5, z: 8 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior15);

const wallInterior16 = createWall(5, 1, 0.2, { x: 0, y: 4.5, z: 2 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior16);
const wallInterior17 = createWall(5, 1, 0.2, { x: 10, y: 4.5, z: 2 }, 'textures/wall.jpg', { x: 0, y: 0, z: 0 }); // Largura, altura, profundidade, posição e caminho da textura
scene.add(wallInterior17);


// Adicionando luz à cena
const ambientLight = new THREE.AmbientLight(0x404040, 1); // Luz suave
scene.add(ambientLight);

// Luz direcional
const directionalLight = new THREE.DirectionalLight(0xffffff, 1); // Luz direcional
directionalLight.position.set(5, 10, 5).normalize(); // Posicionar a luz
scene.add(directionalLight);

// Configuração dos controles de órbita
const controls = new OrbitControls(camera, renderer.domElement);
controls.enableDamping = true;
controls.dampingFactor = 0.25;
controls.screenSpacePanning = false;

camera.position.set(10, 10, 25);

function animate() {
    requestAnimationFrame(animate);

    controls.update(); // Atualiza os controles de órbita

    renderer.render(scene, camera);
}

animate();
