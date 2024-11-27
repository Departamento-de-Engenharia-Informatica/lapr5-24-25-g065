import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import { loadModel } from './models.js';
import { createWall } from './wall.js';
import { createGround } from './ground.js';
import { loadModelWithRotation } from './models.js';

const scene = new THREE.Scene();
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

const plane = createGround({ x: 10, y: 0, z: 0 });
scene.add(plane);
const plane1 = createGround({ x: 10, y: 0, z: 10 });
scene.add(plane1);
const plane2 = createGround({ x: 0, y: 0, z: 0 });
scene.add(plane2);
const plane3 = createGround({ x: 0, y: 0, z: 10 });
scene.add(plane3);

async function loadModelsFromJSON(filePath, scene, loadRotation = false) {
  const response = await fetch(filePath);
  const modelsData = await response.json();

  modelsData.forEach(modelData => {
    if (loadRotation) {
      loadModelWithRotation(scene, modelData.model, modelData.position, modelData.scale, modelData.rotation);
    } else {
      loadModel(scene, modelData.model, modelData.position, modelData.scale);
    }
  });
}

loadModelsFromJSON('ImportedModels.json', scene);
loadModelsFromJSON('ImportedHuman.json', scene, true);

async function loadWalls() {
  const response = await fetch('importedWalls.json');
  const wallsData = await response.json();

  wallsData.forEach(wallData => {
    const { width, height, depth } = wallData.dimensions; // Desestruturando as dimensões
    const wall = createWall(
      width,
      height,
      depth,
      wallData.position,
      wallData.texture,
      wallData.rotation
    );
    scene.add(wall);
  });
}

loadWalls();

const ambientLight = new THREE.AmbientLight(0x404040, 1);
scene.add(ambientLight);

const directionalLight = new THREE.DirectionalLight(0xffffff, 1);
directionalLight.position.set(5, 10, 5).normalize();
scene.add(directionalLight);

const controls = new OrbitControls(camera, renderer.domElement);
controls.enableDamping = true;
controls.dampingFactor = 0.25;
controls.screenSpacePanning = false;


camera.position.set(10, 10, 25);

function animate() {
    requestAnimationFrame(animate);

    controls.update();
    renderer.render(scene, camera);
}

window.addEventListener('resize', () => {
  camera.aspect = window.innerWidth / window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(window.innerWidth, window.innerHeight);
});

animate();
