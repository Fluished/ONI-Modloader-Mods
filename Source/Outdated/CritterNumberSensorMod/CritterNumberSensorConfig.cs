﻿// PressureDoorConfig
using System.Reflection;
using Harmony;
using OverlayModes;
using TUNING;
using UnityEngine;

public class CritterNumberSensorConfig : IBuildingConfig
{
    public const string ID = "CritterNumberSensor";

    public override BuildingDef CreateBuildingDef()
    {
        Debug.Log(" === CritterNumberSensorConfig INI === ");
        int width = 1;
        int height = 1;
        string anim = "diseasesensor_kanim";
        int hitpoints = 30;
		float construction_time = 30f;
		float[] tIER = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] rEFINED_METALS = MATERIALS.REFINED_METALS;
		float melting_point = 1600f;
		BuildLocationRule build_location_rule = BuildLocationRule.Anywhere;
		EffectorValues nONE = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, tIER, rEFINED_METALS, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER0, nONE, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = SimViewMode.Logic;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		SoundEventVolumeCache.instance.AddVolume("diseasesensor_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("diseasesensor_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(Logic.HighlightItemIDs, ID);
		return buildingDef;
    }

    public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
    {
        GeneratedBuildings.RegisterLogicPorts(go, LogicSwitchConfig.OUTPUT_PORT);
    }

    public override void DoPostConfigureUnderConstruction(GameObject go)
    {
        GeneratedBuildings.RegisterLogicPorts(go, LogicSwitchConfig.OUTPUT_PORT);
    }

    public override void DoPostConfigureComplete(GameObject go)
    {
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		GeneratedBuildings.RegisterLogicPorts(go, LogicSwitchConfig.OUTPUT_PORT);
		LogicCritterSensor logicTemperatureSensor = go.AddOrGet<LogicCritterSensor>();
		logicTemperatureSensor.manuallyControlled = false;
		logicTemperatureSensor.minCritters = 0f;
		logicTemperatureSensor.maxCritters = 20f;
	}
    
    
}
