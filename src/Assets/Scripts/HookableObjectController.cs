using UnityEditor.SceneManagement;
using UnityEngine;

/**
 * HookableObjectController contains the functionality of Hookable Objects
 */
public static  class HookableObjectController
{
    /**Manages Collision Event of HookableObject (called by HookableObject Instance)
     *
     * @param hookableObject HookableObject which detected collision
     * @param otherObject GameObject which collided with HookableObject
     */
    public static void OnHookableObjectCollision(HookableObject hookableObject, GameObject gameObject)
    {
    }
    public static void OnHookableObjectCollision(Stone stone, GameObject gameObject)
    {
        if (gameObject.GetType() == typeof(Projectile))
        {
            AttachHookableObjectToProjectile(stone, gameObject);

        }
    }
 
   
    public static void OnHookableObjectCollision(Item item, GameObject gameObject)
    {
        if (gameObject.GetType() == typeof(Projectile))
        {
            AttachHookableObjectToProjectile(item, gameObject);
        }
    }

    private static void AttachHookableObjectToProjectile(HookableObject hookableObject, GameObject projectileGameObject){}
    
    
}