using CustomModT001.Abstract;

namespace CustomModT001;

public class Terraforms : ExtendLibrary<TerraformOptions, Terraforms>
{
    [CloneSource("grenade")] public static TerraformOptions intentional_punching_terra { get; private set; }

    protected override void OnInit()
    {
        RegisterAssets(ModClass.asset_id_prefix);

        intentional_punching_terra.damage = 1;
        intentional_punching_terra.add_burned = true;
        intentional_punching_terra.shake = false;
        intentional_punching_terra.explode_and_set_random_fire = false;
        intentional_punching_terra.explode_strength = 1;
    }
}