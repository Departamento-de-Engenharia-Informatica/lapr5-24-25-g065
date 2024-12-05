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




const raycaster = new THREE.Raycaster();
const mouse = new THREE.Vector2();

const clickableObjects = [];

const targetPositions = [
  { x: -3, y: 2.5, z: -2.5 },
  { x: 2.5, y: 2.5, z: -2.5 },
  { x: 7.5, y: 2.5, z: -2.5 },
  { x: 14, y: 2.5, z: -3 },
  { x: -3, y: 2.5, z: 12.5 },
  { x: 2.5, y: 2.5, z: 12.5 },
  { x: 7.5, y: 2.5, z: 12.5 },
  { x: 14, y: 2.5, z: 12 },
];

const boxPositions = [
  { x: -5, y: 1, z: -5 },
  { x: 2.5, y: 1, z: -5 },
  { x: 8, y: 1, z: -5 },
  { x: 15, y: 1, z: -5 },
  { x: -5, y: 1, z: 15 },
  { x: 2, y: 1, z: 15 },
  { x: 8, y: 1, z: 15 },
  { x: 15, y: 1, z: 15 },
];

const roomData = [
  { name: "Room A", description: "This is room A, a spacious area with modern amenities." },
  { name: "Room B", description: "Room B is ideal for general medical treatments." },
  { name: "Room C", description: "Room C is used for critical care with state-of-the-art equipment." },
  { name: "Room D", description: "Room with more space for surgical processes." },
  { name: "Room E", description: "Room E is a small consultation room." },
  { name: "Room F", description: "Room F is used for minor surgeries." },
  { name: "Room G", description: "Room G is an emergency response room." },
  { name: "Room H", description: "Room with more space for surgical processes" }
];

boxPositions.forEach((pos, index) => {
  const geometry = new THREE.BoxGeometry(4, 2, 2);
  const material = new THREE.MeshStandardMaterial({
    color: 0xff0000,
    transparent: true,
    opacity: 0
  });
  const box = new THREE.Mesh(geometry, material);
  box.position.set(pos.x, pos.y, pos.z);
  scene.add(box);
  clickableObjects.push(box);
});

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
    const { width, height, depth } = wallData.dimensions;
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

const ambientLight = new THREE.AmbientLight(0x444040, 1);
scene.add(ambientLight);

const directionalLight = new THREE.DirectionalLight(0xffffff, 1);
directionalLight.position.set(5, 10, 5).normalize();
scene.add(directionalLight);

const controls = new OrbitControls(camera, renderer.domElement);
controls.enableDamping = true;
controls.dampingFactor = 0.25;
controls.screenSpacePanning = false;
camera.position.set(10, 10, 25);

controls.target.set(5, 5, 5);
controls.mouseButtons = {
  LEFT: null,
  RIGHT: THREE.MOUSE.ROTATE,
  MIDDLE: THREE.MOUSE.DOLLY
};

camera.position.set(10, 10, 25);

let targetCameraPosition = new THREE.Vector3();
let cameraStartPosition = new THREE.Vector3();
let animationStartTime = 0;
const animationDuration = 4000;
let isAnimating = false;

function easeOutQuad(t) {
  return t * (2 - t);
}

function updateCameraPosition() {
  if (!isAnimating) return;

  const elapsedTime = Date.now() - animationStartTime;
  let progress = Math.min(elapsedTime / animationDuration, 1);

  progress = easeOutQuad(progress);

  camera.position.lerpVectors(cameraStartPosition, targetCameraPosition, progress);

  const interpolatedTarget = new THREE.Vector3(
    targetCameraPosition.x,
    targetCameraPosition.y - 5,
    targetCameraPosition.z
  );

  controls.target.lerp(interpolatedTarget, progress);

  if (progress === 1) {
    isAnimating = false;
    controls.target.copy(interpolatedTarget);
  }
}
function animate() {
  requestAnimationFrame(animate);

  updateCameraPosition();

  controls.update();
  renderer.render(scene, camera);
}

let selectedRoomIndex = null;

window.addEventListener('click', (event) => {
  if (event.button !== 0 || isAnimating) return;

  mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
  mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;

  raycaster.setFromCamera(mouse, camera);

  const intersects = raycaster.intersectObjects(clickableObjects);

  if (intersects.length > 0) {
    const index = clickableObjects.indexOf(intersects[0].object);
    selectedRoomIndex = index;

    updateRoomInfoOverlay(selectedRoomIndex);

    const targetPosition = targetPositions[index];

    targetCameraPosition.set(targetPosition.x, targetPosition.y + 10, targetPosition.z);

    cameraStartPosition.copy(camera.position);
    animationStartTime = Date.now();

    isAnimating = true;
  }
});

function updateRoomInfoOverlay(index) {
  const room = roomData[index];
  const roomInfoText = document.getElementById('roomInfoText');
  roomInfoText.textContent = `Room Name: ${room.name}\nDescription: ${room.description}`;
}

function toggleOverlay() {
  const overlay = document.getElementById('roomInfoOverlay');
  overlay.style.display = (overlay.style.display === 'none' || overlay.style.display === '') ? 'block' : 'none';
}

window.addEventListener('keydown', (event) => {
  if (event.key === 'i') {
    toggleOverlay();
  }
});

window.addEventListener('resize', () => {
  camera.aspect = window.innerWidth / window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(window.innerWidth, window.innerHeight);
});

animate();
