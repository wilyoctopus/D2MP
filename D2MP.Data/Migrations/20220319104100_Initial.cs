using FluentMigrator;

namespace D2MP.Data.Migrations
{
    [Migration(20220319104100)]
    public class Initial : Migration
    {
        public override void Up()
        {
            Create.Table("partial_match_results")
                .WithColumn("hero_ids").AsString()
                .WithColumn("won").AsBoolean();

            Create.Table("processed_matches")
                .WithColumn("match_seq_id").AsInt64();

            Create.Table("heroes")
                .WithColumn("id").AsInt16()
                .WithColumn("name").AsAnsiString()
                .WithColumn("localized_name").AsString();

            AddHeroes();
        }

        public override void Down()
        {
            Delete.Table("partial_match_results");
            Delete.Table("processed_matches");
            Delete.Table("heroes");
        }

        private void AddHeroes()
        {
            Insert.IntoTable("heroes").Row(new { id = 1, name = "npc_dota_hero_antimage", localized_name = "Anti-Mage" });
            Insert.IntoTable("heroes").Row(new { id = 2, name = "npc_dota_hero_axe", localized_name = "Axe" });
            Insert.IntoTable("heroes").Row(new { id = 3, name = "npc_dota_hero_bane", localized_name = "Bane" });
            Insert.IntoTable("heroes").Row(new { id = 4, name = "npc_dota_hero_bloodseeker", localized_name = "Bloodseeker" });
            Insert.IntoTable("heroes").Row(new { id = 5, name = "npc_dota_hero_crystal_maiden", localized_name = "Crystal Maiden" });
            Insert.IntoTable("heroes").Row(new { id = 6, name = "npc_dota_hero_drow_ranger", localized_name = "Drow Ranger" });
            Insert.IntoTable("heroes").Row(new { id = 7, name = "npc_dota_hero_earthshaker", localized_name = "Earthshaker" });
            Insert.IntoTable("heroes").Row(new { id = 8, name = "npc_dota_hero_juggernaut", localized_name = "Juggernaut" });
            Insert.IntoTable("heroes").Row(new { id = 9, name = "npc_dota_hero_mirana", localized_name = "Mirana" });
            Insert.IntoTable("heroes").Row(new { id = 11, name = "npc_dota_hero_nevermore", localized_name = "Shadow Fiend" });
            Insert.IntoTable("heroes").Row(new { id = 10, name = "npc_dota_hero_morphling", localized_name = "Morphling" });
            Insert.IntoTable("heroes").Row(new { id = 12, name = "npc_dota_hero_phantom_lancer", localized_name = "Phantom Lancer" });
            Insert.IntoTable("heroes").Row(new { id = 13, name = "npc_dota_hero_puck", localized_name = "Puck" });
            Insert.IntoTable("heroes").Row(new { id = 14, name = "npc_dota_hero_pudge", localized_name = "Pudge" });
            Insert.IntoTable("heroes").Row(new { id = 15, name = "npc_dota_hero_razor", localized_name = "Razor" });
            Insert.IntoTable("heroes").Row(new { id = 16, name = "npc_dota_hero_sand_king", localized_name = "Sand King" });
            Insert.IntoTable("heroes").Row(new { id = 17, name = "npc_dota_hero_storm_spirit", localized_name = "Storm Spirit" });
            Insert.IntoTable("heroes").Row(new { id = 18, name = "npc_dota_hero_sven", localized_name = "Sven" });
            Insert.IntoTable("heroes").Row(new { id = 19, name = "npc_dota_hero_tiny", localized_name = "Tiny" });
            Insert.IntoTable("heroes").Row(new { id = 20, name = "npc_dota_hero_vengefulspirit", localized_name = "Vengeful Spirit" });
            Insert.IntoTable("heroes").Row(new { id = 21, name = "npc_dota_hero_windrunner", localized_name = "Windranger" });
            Insert.IntoTable("heroes").Row(new { id = 22, name = "npc_dota_hero_zuus", localized_name = "Zeus" });
            Insert.IntoTable("heroes").Row(new { id = 23, name = "npc_dota_hero_kunkka", localized_name = "Kunkka" });
            Insert.IntoTable("heroes").Row(new { id = 25, name = "npc_dota_hero_lina", localized_name = "Lina" });
            Insert.IntoTable("heroes").Row(new { id = 31, name = "npc_dota_hero_lich", localized_name = "Lich" });
            Insert.IntoTable("heroes").Row(new { id = 26, name = "npc_dota_hero_lion", localized_name = "Lion" });
            Insert.IntoTable("heroes").Row(new { id = 27, name = "npc_dota_hero_shadow_shaman", localized_name = "Shadow Shaman" });
            Insert.IntoTable("heroes").Row(new { id = 28, name = "npc_dota_hero_slardar", localized_name = "Slardar" });
            Insert.IntoTable("heroes").Row(new { id = 29, name = "npc_dota_hero_tidehunter", localized_name = "Tidehunter" });
            Insert.IntoTable("heroes").Row(new { id = 30, name = "npc_dota_hero_witch_doctor", localized_name = "Witch Doctor" });
            Insert.IntoTable("heroes").Row(new { id = 32, name = "npc_dota_hero_riki", localized_name = "Riki" });
            Insert.IntoTable("heroes").Row(new { id = 33, name = "npc_dota_hero_enigma", localized_name = "Enigma" });
            Insert.IntoTable("heroes").Row(new { id = 34, name = "npc_dota_hero_tinker", localized_name = "Tinker" });
            Insert.IntoTable("heroes").Row(new { id = 35, name = "npc_dota_hero_sniper", localized_name = "Sniper" });
            Insert.IntoTable("heroes").Row(new { id = 36, name = "npc_dota_hero_necrolyte", localized_name = "Necrophos" });
            Insert.IntoTable("heroes").Row(new { id = 37, name = "npc_dota_hero_warlock", localized_name = "Warlock" });
            Insert.IntoTable("heroes").Row(new { id = 38, name = "npc_dota_hero_beastmaster", localized_name = "Beastmaster" });
            Insert.IntoTable("heroes").Row(new { id = 39, name = "npc_dota_hero_queenofpain", localized_name = "Queen of Pain" });
            Insert.IntoTable("heroes").Row(new { id = 40, name = "npc_dota_hero_venomancer", localized_name = "Venomancer" });
            Insert.IntoTable("heroes").Row(new { id = 41, name = "npc_dota_hero_faceless_void", localized_name = "Faceless Void" });
            Insert.IntoTable("heroes").Row(new { id = 42, name = "npc_dota_hero_skeleton_king", localized_name = "Wraith King" });
            Insert.IntoTable("heroes").Row(new { id = 43, name = "npc_dota_hero_death_prophet", localized_name = "Death Prophet" });
            Insert.IntoTable("heroes").Row(new { id = 44, name = "npc_dota_hero_phantom_assassin", localized_name = "Phantom Assassin" });
            Insert.IntoTable("heroes").Row(new { id = 45, name = "npc_dota_hero_pugna", localized_name = "Pugna" });
            Insert.IntoTable("heroes").Row(new { id = 46, name = "npc_dota_hero_templar_assassin", localized_name = "Templar Assassin" });
            Insert.IntoTable("heroes").Row(new { id = 47, name = "npc_dota_hero_viper", localized_name = "Viper" });
            Insert.IntoTable("heroes").Row(new { id = 48, name = "npc_dota_hero_luna", localized_name = "Luna" });
            Insert.IntoTable("heroes").Row(new { id = 49, name = "npc_dota_hero_dragon_knight", localized_name = "Dragon Knight" });
            Insert.IntoTable("heroes").Row(new { id = 50, name = "npc_dota_hero_dazzle", localized_name = "Dazzle" });
            Insert.IntoTable("heroes").Row(new { id = 51, name = "npc_dota_hero_rattletrap", localized_name = "Clockwerk" });
            Insert.IntoTable("heroes").Row(new { id = 52, name = "npc_dota_hero_leshrac", localized_name = "Leshrac" });
            Insert.IntoTable("heroes").Row(new { id = 53, name = "npc_dota_hero_furion", localized_name = "Nature's Prophet" });
            Insert.IntoTable("heroes").Row(new { id = 54, name = "npc_dota_hero_life_stealer", localized_name = "Lifestealer" });
            Insert.IntoTable("heroes").Row(new { id = 55, name = "npc_dota_hero_dark_seer", localized_name = "Dark Seer" });
            Insert.IntoTable("heroes").Row(new { id = 56, name = "npc_dota_hero_clinkz", localized_name = "Clinkz" });
            Insert.IntoTable("heroes").Row(new { id = 57, name = "npc_dota_hero_omniknight", localized_name = "Omniknight" });
            Insert.IntoTable("heroes").Row(new { id = 58, name = "npc_dota_hero_enchantress", localized_name = "Enchantress" });
            Insert.IntoTable("heroes").Row(new { id = 59, name = "npc_dota_hero_huskar", localized_name = "Huskar" });
            Insert.IntoTable("heroes").Row(new { id = 60, name = "npc_dota_hero_night_stalker", localized_name = "Night Stalker" });
            Insert.IntoTable("heroes").Row(new { id = 61, name = "npc_dota_hero_broodmother", localized_name = "Broodmother" });
            Insert.IntoTable("heroes").Row(new { id = 62, name = "npc_dota_hero_bounty_hunter", localized_name = "Bounty Hunter" });
            Insert.IntoTable("heroes").Row(new { id = 63, name = "npc_dota_hero_weaver", localized_name = "Weaver" });
            Insert.IntoTable("heroes").Row(new { id = 64, name = "npc_dota_hero_jakiro", localized_name = "Jakiro" });
            Insert.IntoTable("heroes").Row(new { id = 65, name = "npc_dota_hero_batrider", localized_name = "Batrider" });
            Insert.IntoTable("heroes").Row(new { id = 66, name = "npc_dota_hero_chen", localized_name = "Chen" });
            Insert.IntoTable("heroes").Row(new { id = 67, name = "npc_dota_hero_spectre", localized_name = "Spectre" });
            Insert.IntoTable("heroes").Row(new { id = 69, name = "npc_dota_hero_doom_bringer", localized_name = "Doom" });
            Insert.IntoTable("heroes").Row(new { id = 68, name = "npc_dota_hero_ancient_apparition", localized_name = "Ancient Apparition" });
            Insert.IntoTable("heroes").Row(new { id = 70, name = "npc_dota_hero_ursa", localized_name = "Ursa" });
            Insert.IntoTable("heroes").Row(new { id = 71, name = "npc_dota_hero_spirit_breaker", localized_name = "Spirit Breaker" });
            Insert.IntoTable("heroes").Row(new { id = 72, name = "npc_dota_hero_gyrocopter", localized_name = "Gyrocopter" });
            Insert.IntoTable("heroes").Row(new { id = 73, name = "npc_dota_hero_alchemist", localized_name = "Alchemist" });
            Insert.IntoTable("heroes").Row(new { id = 74, name = "npc_dota_hero_invoker", localized_name = "Invoker" });
            Insert.IntoTable("heroes").Row(new { id = 75, name = "npc_dota_hero_silencer", localized_name = "Silencer" });
            Insert.IntoTable("heroes").Row(new { id = 76, name = "npc_dota_hero_obsidian_destroyer", localized_name = "Outworld Destroyer" });
            Insert.IntoTable("heroes").Row(new { id = 77, name = "npc_dota_hero_lycan", localized_name = "Lycan" });
            Insert.IntoTable("heroes").Row(new { id = 78, name = "npc_dota_hero_brewmaster", localized_name = "Brewmaster" });
            Insert.IntoTable("heroes").Row(new { id = 79, name = "npc_dota_hero_shadow_demon", localized_name = "Shadow Demon" });
            Insert.IntoTable("heroes").Row(new { id = 80, name = "npc_dota_hero_lone_druid", localized_name = "Lone Druid" });
            Insert.IntoTable("heroes").Row(new { id = 81, name = "npc_dota_hero_chaos_knight", localized_name = "Chaos Knight" });
            Insert.IntoTable("heroes").Row(new { id = 82, name = "npc_dota_hero_meepo", localized_name = "Meepo" });
            Insert.IntoTable("heroes").Row(new { id = 83, name = "npc_dota_hero_treant", localized_name = "Treant Protector" });
            Insert.IntoTable("heroes").Row(new { id = 84, name = "npc_dota_hero_ogre_magi", localized_name = "Ogre Magi" });
            Insert.IntoTable("heroes").Row(new { id = 85, name = "npc_dota_hero_undying", localized_name = "Undying" });
            Insert.IntoTable("heroes").Row(new { id = 86, name = "npc_dota_hero_rubick", localized_name = "Rubick" });
            Insert.IntoTable("heroes").Row(new { id = 87, name = "npc_dota_hero_disruptor", localized_name = "Disruptor" });
            Insert.IntoTable("heroes").Row(new { id = 88, name = "npc_dota_hero_nyx_assassin", localized_name = "Nyx Assassin" });
            Insert.IntoTable("heroes").Row(new { id = 89, name = "npc_dota_hero_naga_siren", localized_name = "Naga Siren" });
            Insert.IntoTable("heroes").Row(new { id = 90, name = "npc_dota_hero_keeper_of_the_light", localized_name = "Keeper of the Light" });
            Insert.IntoTable("heroes").Row(new { id = 91, name = "npc_dota_hero_wisp", localized_name = "Io" });
            Insert.IntoTable("heroes").Row(new { id = 92, name = "npc_dota_hero_visage", localized_name = "Visage" });
            Insert.IntoTable("heroes").Row(new { id = 93, name = "npc_dota_hero_slark", localized_name = "Slark" });
            Insert.IntoTable("heroes").Row(new { id = 94, name = "npc_dota_hero_medusa", localized_name = "Medusa" });
            Insert.IntoTable("heroes").Row(new { id = 95, name = "npc_dota_hero_troll_warlord", localized_name = "Troll Warlord" });
            Insert.IntoTable("heroes").Row(new { id = 96, name = "npc_dota_hero_centaur", localized_name = "Centaur Warrunner" });
            Insert.IntoTable("heroes").Row(new { id = 97, name = "npc_dota_hero_magnataur", localized_name = "Magnus" });
            Insert.IntoTable("heroes").Row(new { id = 98, name = "npc_dota_hero_shredder", localized_name = "Timbersaw" });
            Insert.IntoTable("heroes").Row(new { id = 99, name = "npc_dota_hero_bristleback", localized_name = "Bristleback" });
            Insert.IntoTable("heroes").Row(new { id = 100, name = "npc_dota_hero_tusk", localized_name = "Tusk" });
            Insert.IntoTable("heroes").Row(new { id = 101, name = "npc_dota_hero_skywrath_mage", localized_name = "Skywrath Mage" });
            Insert.IntoTable("heroes").Row(new { id = 102, name = "npc_dota_hero_abaddon", localized_name = "Abaddon" });
            Insert.IntoTable("heroes").Row(new { id = 103, name = "npc_dota_hero_elder_titan", localized_name = "Elder Titan" });
            Insert.IntoTable("heroes").Row(new { id = 104, name = "npc_dota_hero_legion_commander", localized_name = "Legion Commander" });
            Insert.IntoTable("heroes").Row(new { id = 106, name = "npc_dota_hero_ember_spirit", localized_name = "Ember Spirit" });
            Insert.IntoTable("heroes").Row(new { id = 107, name = "npc_dota_hero_earth_spirit", localized_name = "Earth Spirit" });
            Insert.IntoTable("heroes").Row(new { id = 109, name = "npc_dota_hero_terrorblade", localized_name = "Terrorblade" });
            Insert.IntoTable("heroes").Row(new { id = 110, name = "npc_dota_hero_phoenix", localized_name = "Phoenix" });
            Insert.IntoTable("heroes").Row(new { id = 111, name = "npc_dota_hero_oracle", localized_name = "Oracle" });
            Insert.IntoTable("heroes").Row(new { id = 105, name = "npc_dota_hero_techies", localized_name = "Techies" });
            Insert.IntoTable("heroes").Row(new { id = 112, name = "npc_dota_hero_winter_wyvern", localized_name = "Winter Wyvern" });
            Insert.IntoTable("heroes").Row(new { id = 113, name = "npc_dota_hero_arc_warden", localized_name = "Arc Warden" });
            Insert.IntoTable("heroes").Row(new { id = 108, name = "npc_dota_hero_abyssal_underlord", localized_name = "Underlord" });
            Insert.IntoTable("heroes").Row(new { id = 114, name = "npc_dota_hero_monkey_king", localized_name = "Monkey King" });
            Insert.IntoTable("heroes").Row(new { id = 120, name = "npc_dota_hero_pangolier", localized_name = "Pangolier" });
            Insert.IntoTable("heroes").Row(new { id = 119, name = "npc_dota_hero_dark_willow", localized_name = "Dark Willow" });
            Insert.IntoTable("heroes").Row(new { id = 121, name = "npc_dota_hero_grimstroke", localized_name = "Grimstroke" });
            Insert.IntoTable("heroes").Row(new { id = 129, name = "npc_dota_hero_mars", localized_name = "Mars" });
            Insert.IntoTable("heroes").Row(new { id = 126, name = "npc_dota_hero_void_spirit", localized_name = "Void Spirit" });
            Insert.IntoTable("heroes").Row(new { id = 128, name = "npc_dota_hero_snapfire", localized_name = "Snapfire" });
            Insert.IntoTable("heroes").Row(new { id = 123, name = "npc_dota_hero_hoodwink", localized_name = "Hoodwink" });
            Insert.IntoTable("heroes").Row(new { id = 135, name = "npc_dota_hero_dawnbreaker", localized_name = "Dawnbreaker" });
            Insert.IntoTable("heroes").Row(new { id = 136, name = "npc_dota_hero_marci", localized_name = "Marci" });
            Insert.IntoTable("heroes").Row(new { id = 137, name = "npc_dota_hero_primal_beast", localized_name = "Primal Beast" });
        }
    }
}
