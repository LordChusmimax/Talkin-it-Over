using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Gun : Weapon
{
    // Reference to the sprite mesh - quad.
    [SerializeField]
    private Mesh spriteMesh;
    // Reference to the material with sprite texture.
    [SerializeField]
    private Material spriteMaterial;

    private EntityManager entityManager;
    private EntityArchetype archetype;
    private Entity entity; 



    // Start is called before the first frame update
    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        archetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(LocalToWorld),
            typeof(RenderMesh)
            );
        entity = entityManager.CreateEntity(archetype);
        entityManager.AddComponentData<Translation>(entity, new Translation() { Value = transform.position });
        entityManager.SetSharedComponentData(entity, new RenderMesh { mesh = spriteMesh, material = spriteMaterial});
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void onPick()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }

}
