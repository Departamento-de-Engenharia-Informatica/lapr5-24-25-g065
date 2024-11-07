import * as THREE from "three";

/*
 * parameters = {
 *  textureUrl: String,
 *  size: Vector2
 * }
 */

export default class Ground {
    constructor(parameters) {
        for (const [key, value] of Object.entries(parameters)) {
            this[key] = value;
        }

        // Cria um carregador de textura
        const textureLoader = new THREE.TextureLoader();

        // Carrega a textura
        this.texture = textureLoader.load(this.textureUrl, (texture) => {
            console.log("Textura do chão carregada com sucesso!");
            // Aqui você pode adicionar a mesh ao cenário se a textura for carregada
            this.createGround();
        }, undefined, (error) => {
            console.error("Erro ao carregar a textura:", error);
        });
    }

    createGround() {
        // Configuração da textura
        this.texture.colorSpace = THREE.SRGBColorSpace;
        this.texture.wrapS = THREE.RepeatWrapping;
        this.texture.wrapT = THREE.RepeatWrapping;
        this.texture.repeat.set(this.size.width, this.size.height);
        this.texture.magFilter = THREE.LinearFilter;
        this.texture.minFilter = THREE.LinearMipmapLinearFilter;

        // Criação do chão
        const geometry = new THREE.PlaneGeometry(this.size.width, this.size.height);
        const material = new THREE.MeshPhongMaterial({ color: 0xffffff, map: this.texture });
        this.object = new THREE.Mesh(geometry, material);
        this.object.rotation.x = -Math.PI / 2.0; // Rotaciona o chão para ficar plano
        this.object.castShadow = false; // Não projeta sombras
        this.object.receiveShadow = true; // Recebe sombras

        // Adiciona o chão à cena
        scene.add(this.object); // Certifique-se de que a variável `scene` está acessível aqui
    }
}