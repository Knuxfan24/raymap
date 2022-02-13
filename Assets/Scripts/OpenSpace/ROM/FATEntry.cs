﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpace.ROM {
	public class FATEntry {
		public LegacyPointer offset;
		public uint off_data;
		public ushort type;
		public ushort index;

		// Calculated
		public uint tableIndex;
		public uint entryIndexWithinTable;
		public uint size;

		public static FATEntry Read(Reader reader, LegacyPointer offset) {
			FATEntry entry = new FATEntry();
			entry.offset = offset;
			entry.off_data = reader.ReadUInt32();
			entry.type = reader.ReadUInt16();
			entry.index = reader.ReadUInt16();
			return entry;
		}

		public static Dictionary<ushort, Type> TypesDS = new Dictionary<ushort, Type>() {
			{ 0, Type.EngineStruct },
			{ 1, Type.Perso },
			{ 2, Type.Comport },
			{ 3, Type.AIModel },
			{ 4, Type.Family },
			{ 5, Type.State },
			{ 6, Type.ObjectsTable },
			{ 7, Type.EntryAction },
			{ 8, Type.AnimationReference },
			{ 9, Type.Perso3dData },
			{ 10, Type.ActivationList },
			{ 11, Type.ActivationZone },
			{ 12, Type.WayPoint },
			{ 13, Type.Graph },
			{ 14, Type.GraphNode },
			{ 15, Type.ZdxList },
			{ 16, Type.LightInfo },
			{ 17, Type.TextureInfoRef }, // size: 0x2
			{ 22, Type.VertexArray },
			{ 23, Type.GeometricElementTrianglesData },
			{ 24, Type.GeometricElementTriangles },
			{ 25, Type.GeometricElementSprites },
			{ 26, Type.GeometricElementCollideSpheres },
			{ 27, Type.GeometricElementCollideAlignedBoxes },
			{ 31, Type.GeometricObject },
			{ 32, Type.GameMaterial },
			{ 33, Type.CollideMaterial },
			{ 34, Type.VisualMaterial },
			{ 37, Type.MechanicsMaterial },
			{ 38, Type.PhysicalCollSet },
			{ 39, Type.PhysicalObject },
			{ 40, Type.Sector },
			{ 41, Type.SuperObject },
			{ 42, Type.GeometricELementCollideTrianglesData },
			{ 43, Type.LevelList },
			{ 44, Type.LevelHeader },
			{ 45, Type.HierarchyRoot },
			{ 46, Type.ShortArray },
			{ 47, Type.SuperObjectDynamic },
			{ 48, Type.SuperObjectDynamicArray },
			{ 49, Type.SuperObjectArray },
			{ 50, Type.CompressedVector3Array },
			{ 53, Type.SectorSuperObjectArray1_and_PersoArray },
			{ 54, Type.SectorSuperObjectArray2 },
			{ 55, Type.SectorSuperObjectArray3 },
			{ 56, Type.LightInfoArray },
			{ 57, Type.SectorSuperObjectArray4 },
			{ 58, Type.SectorSuperObjectArray4Info },
			{ 59, Type.SectorSuperObjectArray5 },
			{ 60, Type.SectorSuperObjectArray5Info },
            { 63, Type.TextureInfo }, // size: 14
            { 64, Type.DsgVar },
            { 65, Type.StateTransitionArray },
            { 66, Type.Brain },
			{ 67, Type.Camera },
			{ 68, Type.CollSet },
			{ 70, Type.PersoMatrixAndVector },
			{ 71, Type.StdGame },
			{ 75, Type.ObjectsTableData },
			{ 76, Type.DsgVarInfo },
			{ 77, Type.ComportList },
			{ 78, Type.ComportArray },
			{ 79, Type.ScriptArray },
			{ 80, Type.Script },
			{ 82, Type.ScriptNodeArray },
			{ 83, Type.GeometricObjectArray },
			{ 84, Type.GeometricElementCollideAlignedBoxArray },
			{ 85, Type.GeometricElementCollideSphereArray },
			{ 88, Type.ActivationZoneArray },
			{ 89, Type.ActivationIndexArray },
			{ 91, Type.NoCtrlTextureList_and_GraphNodeArray },
			{ 92, Type.ArcCapsArray },
			{ 93, Type.ArcWeightArray },
			{ 97, Type.SectorSuperObjectArray1Info },
			{ 99, Type.Scr_Int },
			{ 100, Type.Scr_Real },
			{ 101, Type.Scr_Vector3 },
			{ 102, Type.Scr_String },
			{ 103, Type.Dsg_Int },
			{ 104, Type.Dsg_UInt },
			{ 105, Type.Dsg_Real },
			{ 106, Type.Dsg_Vector3 },
			{ 107, Type.StateArrayRef },
			{ 108, Type.StateArray },
			{ 111, Type.DsgMem },
			{ 112, Type.DsgMemInfo },
			{ 113, Type.DsgMemInfoArray },
			{ 118, Type.InputStruct },
			{ 119, Type.EntryActionArray },
			{ 120, Type.KeyWordArray },
			{ 127, Type.Vector3Array },
			{ 128, Type.Short3Array },
			{ 132, Type.NumLanguages },
			{ 133, Type.StringRef },
			{ 134, Type.LanguageTable },
			{ 135, Type.StringRefArray },
			{ 136, Type.BinaryStringRef },
			{ 137, Type.BinaryStringRefArray },
			{ 145, Type.PersosInFixList },
			{ 148, Type.GeometricElementVisualList },
			{ 149, Type.GeometricElementCollideTriangles },
			{ 150, Type.GeometricElementCollideList },
			{ 156, Type.VisualMaterialTextures },
			{ 157, Type.String },
			{ 158, Type.DsgVarEntry },
		};

		public static Dictionary<ushort, Type> TypesN64 = new Dictionary<ushort, Type>() {
			{ 0, Type.EngineStruct },
			{ 1, Type.Perso },
			{ 2, Type.Comport },
			{ 3, Type.AIModel },
            { 4, Type.Family },
			{ 5, Type.State },
			{ 6, Type.ObjectsTable },
			{ 7, Type.EntryAction },
			{ 8, Type.AnimationReference },
			{ 9, Type.Perso3dData },
			{ 10, Type.ActivationList },
			{ 11, Type.ActivationZone },
			{ 12, Type.WayPoint },
			{ 13, Type.Graph },
			{ 14, Type.GraphNode },
			{ 15, Type.ZdxList },
			{ 16, Type.LightInfo },
			{ 17, Type.TextureInfoRef }, // size: 0x2
			{ 21, Type.Palette },
			{ 22, Type.VertexArray },
			{ 23, Type.GeometricElementTrianglesData },
			{ 24, Type.GeometricElementTriangles },
			{ 25, Type.GeometricElementSprites },
			{ 26, Type.GeometricElementCollideSpheres },
			{ 27, Type.GeometricElementCollideAlignedBoxes },
			{ 31, Type.GeometricObject },
			{ 32, Type.GameMaterial },
			{ 33, Type.CollideMaterial },
			{ 34, Type.VisualMaterial },
			{ 37, Type.MechanicsMaterial },
			{ 38, Type.PhysicalCollSet },
			{ 39, Type.PhysicalObject },
			{ 40, Type.Sector },
			{ 41, Type.SuperObject },
			{ 42, Type.GeometricELementCollideTrianglesData },
			{ 43, Type.LevelList },
			{ 44, Type.LevelHeader },
			{ 45, Type.HierarchyRoot },
			{ 46, Type.ShortArray },
			{ 47, Type.SuperObjectDynamic },
			{ 48, Type.SuperObjectDynamicArray },
			{ 49, Type.SuperObjectArray },
			{ 50, Type.CompressedVector3Array },
			{ 53, Type.SectorSuperObjectArray1_and_PersoArray },
			{ 54, Type.SectorSuperObjectArray2 },
			{ 55, Type.SectorSuperObjectArray3 },
			{ 56, Type.LightInfoArray },
			{ 57, Type.SectorSuperObjectArray4 },
			{ 58, Type.SectorSuperObjectArray4Info },
			{ 59, Type.SectorSuperObjectArray5 },
			{ 60, Type.SectorSuperObjectArray5Info },
            { 63, Type.TextureInfo }, // size: 12
            { 64, Type.DsgVar },
            { 65, Type.StateTransitionArray },
            { 66, Type.Brain },
			{ 67, Type.Camera },
			{ 68, Type.CollSet },
			{ 70, Type.PersoMatrixAndVector },
			{ 71, Type.StdGame },
			{ 75, Type.ObjectsTableData },
			{ 76, Type.DsgVarInfo },
			{ 77, Type.ComportList },
			{ 78, Type.ComportArray },
			{ 79, Type.ScriptArray },
			{ 80, Type.Script },
			{ 82, Type.ScriptNodeArray },
			{ 83, Type.GeometricObjectArray },
			{ 84, Type.GeometricElementCollideAlignedBoxArray },
			{ 85, Type.GeometricElementCollideSphereArray },
			{ 88, Type.ActivationZoneArray },
			{ 89, Type.ActivationIndexArray },
			{ 91, Type.NoCtrlTextureList_and_GraphNodeArray },
			{ 92, Type.ArcCapsArray },
			{ 93, Type.ArcWeightArray },
			{ 97, Type.SectorSuperObjectArray1Info },
			{ 99, Type.Scr_Int },
			{ 100, Type.Scr_Real },
			{ 101, Type.Scr_Vector3 },
			{ 102, Type.Scr_String },
			{ 103, Type.Dsg_Int },
			{ 104, Type.Dsg_UInt },
			{ 105, Type.Dsg_Real },
			{ 106, Type.Dsg_Vector3 },
			{ 107, Type.StateArrayRef },
			{ 108, Type.StateArray },
			{ 111, Type.DsgMem },
			{ 112, Type.DsgMemInfo },
			{ 113, Type.DsgMemInfoArray },
			{ 118, Type.InputStruct },
			{ 119, Type.EntryActionArray },
			{ 120, Type.KeyWordArray },
			{ 127, Type.Vector3Array },
			{ 128, Type.Short3Array },
			{ 132, Type.NumLanguages },
			{ 133, Type.StringRef },
			{ 134, Type.LanguageTable },
			{ 135, Type.StringRefArray },
			{ 136, Type.BinaryStringRef },
			{ 137, Type.BinaryStringRefArray },
			{ 145, Type.PersosInFixList },
			{ 148, Type.GeometricElementVisualList },
			{ 149, Type.GeometricElementCollideTriangles },
			{ 150, Type.GeometricElementCollideList },
			{ 156, Type.VisualMaterialTextures },
			{ 157, Type.String },
			{ 158, Type.DsgVarEntry },
		};

		public static Dictionary<ushort, Type> Types3DS = new Dictionary<ushort, Type>() {
			{ 0, Type.EngineStruct },
			{ 1, Type.Perso },
			{ 2, Type.Comport },
			{ 3, Type.AIModel },
			{ 4, Type.Family },
			{ 5, Type.State },
			{ 6, Type.ObjectsTable },
			{ 7, Type.EntryAction },
			{ 8, Type.AnimationReference },
			{ 9, Type.Perso3dData },
			{ 10, Type.ActivationList },
			{ 11, Type.ActivationZone },
			{ 12, Type.WayPoint },
			{ 13, Type.Graph },
			{ 14, Type.GraphNode },
			{ 15, Type.ZdxList },
			{ 16, Type.LightInfo },
			{ 17, Type.TextureInfoRef }, // size: 0x2
			{ 18, Type.VertexArray },
			{ 19, Type.GeometricElementTrianglesData },
			{ 20, Type.GeometricElementTriangles },
			{ 21, Type.GeometricElementSprites },
			{ 22, Type.GeometricElementCollideSpheres },
			{ 23, Type.GeometricElementCollideAlignedBoxes },
			{ 27, Type.GeometricObject },
			{ 28, Type.GameMaterial },
			{ 29, Type.CollideMaterial },
			{ 30, Type.VisualMaterial },
			{ 33, Type.MechanicsMaterial },
			{ 34, Type.PhysicalCollSet },
			{ 35, Type.PhysicalObject },
			{ 36, Type.Sector },
			{ 37, Type.SuperObject },
			{ 38, Type.GeometricELementCollideTrianglesData },
			{ 39, Type.LevelList },
			{ 40, Type.LevelHeader },
			{ 41, Type.HierarchyRoot },
			{ 42, Type.ShortArray },
			{ 43, Type.SuperObjectDynamic },
			{ 44, Type.SuperObjectDynamicArray },
			{ 45, Type.SuperObjectArray },
			{ 46, Type.CompressedVector3Array },
			{ 49, Type.SectorSuperObjectArray1_and_PersoArray },
			{ 50, Type.SectorSuperObjectArray2 },
			{ 51, Type.SectorSuperObjectArray3 },
			{ 52, Type.LightInfoArray },
			{ 53, Type.SectorSuperObjectArray4 },
			{ 54, Type.SectorSuperObjectArray4Info },
			{ 55, Type.SectorSuperObjectArray5 },
			{ 56, Type.SectorSuperObjectArray5Info },
			{ 59, Type.TextureInfo }, // size: 0x100D2. contains the actual texture data too!
            { 60, Type.DsgVar },
            { 61, Type.StateTransitionArray },
            { 62, Type.Brain },
			{ 63, Type.Camera },
			{ 64, Type.CollSet },
			{ 66, Type.PersoMatrixAndVector },
			{ 67, Type.StdGame },
			{ 71, Type.ObjectsTableData },
			{ 72, Type.DsgVarInfo },
			{ 73, Type.ComportList },
			{ 74, Type.ComportArray },
			{ 75, Type.ScriptArray },
			{ 76, Type.Script },
			{ 78, Type.ScriptNodeArray },
			{ 79, Type.GeometricObjectArray },
			{ 80, Type.GeometricElementCollideAlignedBoxArray },
			{ 81, Type.GeometricElementCollideSphereArray },
			{ 84, Type.ActivationZoneArray },
			{ 85, Type.ActivationIndexArray },
			{ 87, Type.NoCtrlTextureList_and_GraphNodeArray },
			{ 88, Type.ArcCapsArray },
			{ 89, Type.ArcWeightArray },
			{ 93, Type.SectorSuperObjectArray1Info },
			{ 95, Type.Scr_Int },
			{ 96, Type.Scr_Real },
			{ 97, Type.Scr_Vector3 },
			{ 98, Type.Scr_String },
			{ 99, Type.Dsg_Int },
			{ 100, Type.Dsg_UInt },
			{ 101, Type.Dsg_Real },
			{ 102, Type.Dsg_Vector3 },
			{ 103, Type.StateArrayRef },
			{ 104, Type.StateArray },
			{ 107, Type.DsgMem },
			{ 108, Type.DsgMemInfo },
			{ 109, Type.DsgMemInfoArray },
			{ 114, Type.InputStruct },
			{ 115, Type.EntryActionArray },
			{ 116, Type.KeyWordArray },
            { 123, Type.Vector3Array },
			{ 124, Type.Short3Array },
			{ 128, Type.NumLanguages },
			{ 129, Type.StringRef },
			{ 130, Type.LanguageTable },
			{ 131, Type.StringRefArray },
			{ 132, Type.BinaryStringRef },
			{ 133, Type.BinaryStringRefArray },
			{ 141, Type.PersosInFixList },
			{ 144, Type.GeometricElementVisualList },
			{ 145, Type.GeometricElementCollideTriangles },
			{ 146, Type.GeometricElementCollideList },
			{ 152, Type.VisualMaterialTextures },
			{ 153, Type.String },
			{ 154, Type.DsgVarEntry },
		};

		public enum Flag {
			Fix = 0x8000
		}

		public enum Type {
			Unknown,
			EngineStruct,
			LevelList, // Size: 0x40 * num_levels (0x46 or 70 in Rayman 2)
			LevelHeader, // Size: 0x38
			NumLanguages,
			LanguageTable,
			StringRefArray,
			StringRef,
			String,
			BinaryStringRef,
			BinaryStringRefArray,
			TextureInfo,
			TextureInfoRef,
			VisualMaterialTextures,
			NoCtrlTextureList_and_GraphNodeArray,
			VisualMaterial,
			Palette,
			VertexArray,
			GeometricElementTrianglesData,
			GeometricElementTriangles,
			GeometricElementSprites,
			GeometricObject,
			CompressedVector3Array,
			GeometricElementCollideList,
			GeometricElementVisualList,
			PhysicalObject,
			PhysicalCollSet,
			Vector3Array,
			Short3Array,
			ObjectsTableData,
			ObjectsTable,
			Family,
			GeometricELementCollideTrianglesData,
			GeometricElementCollideTriangles,
			GeometricElementCollideSpheres,
			GeometricElementCollideSphereArray,
			GeometricElementCollideAlignedBoxes,
			GeometricElementCollideAlignedBoxArray,
			GameMaterial,
			CollideMaterial,
			MechanicsMaterial,
			SuperObject,
			Sector,
			SuperObjectArray,
			HierarchyRoot,
			ShortArray,
			SuperObjectDynamic,
			SuperObjectDynamicArray,
			Perso,
			Perso3dData,
			State,
			AnimationReference,
			StdGame,
			StateArrayRef,
			StateArray,
			StateTransitionArray,
			SectorSuperObjectArray1_and_PersoArray,
			SectorSuperObjectArray1Info,
			SectorSuperObjectArray2,
			SectorSuperObjectArray3,
			SectorSuperObjectArray4,
			SectorSuperObjectArray4Info,
			SectorSuperObjectArray5,
			SectorSuperObjectArray5Info,
			LightInfoArray,
			LightInfo,
            Brain,
            AIModel,
            DsgVar,
			Comport,
			ComportArray,
			Script,
			ScriptArray,
			ScriptNodeArray,
			ComportList,
			Scr_String,
			Graph,
			GraphNode,
			WayPoint,
			ArcWeightArray,
			ArcCapsArray,
			CollSet,
			ZdxList,
			ActivationList,
			ActivationZoneArray,
			ActivationZone,
			ActivationIndexArray,
			GeometricObjectArray,
			Scr_Vector3,
			Scr_Int,
			Scr_Real,
			EntryAction,
			KeyWordArray,
			EntryActionArray,
			InputStruct,
			DsgVarInfo,
			DsgVarEntry,
			Dsg_Int,
			Dsg_UInt,
			Dsg_Real,
			Dsg_Vector3,
			DsgMem,
			DsgMemInfo,
			DsgMemInfoArray,
			PersosInFixList,
			Camera,
			PersoMatrixAndVector,
		}

		public static Dictionary<System.Type, Type> types = new Dictionary<System.Type, Type>() {
			{ typeof(TextureInfo), Type.TextureInfo },
			{ typeof(TextureInfoRef), Type.TextureInfoRef },
			{ typeof(VisualMaterial), Type.VisualMaterial },
			{ typeof(VisualMaterialTextures), Type.VisualMaterialTextures },
			{ typeof(StringRef), Type.StringRef },
			{ typeof(String), Type.String },
			{ typeof(StringRefArray), Type.StringRefArray },
			{ typeof(BinaryStringRefArray), Type.BinaryStringRefArray },
			{ typeof(BinaryStringRef), Type.BinaryStringRef },
			{ typeof(LanguageTable), Type.LanguageTable },
			{ typeof(NumLanguages), Type.NumLanguages },
			{ typeof(NoCtrlTextureList), Type.NoCtrlTextureList_and_GraphNodeArray },
			{ typeof(EngineStruct), Type.EngineStruct },
			{ typeof(GeometricObjectElementSprites), Type.GeometricElementSprites },
			{ typeof(GeometricObjectElementTriangles), Type.GeometricElementTriangles },
			{ typeof(GeometricObjectElementTrianglesData), Type.GeometricElementTrianglesData },
			{ typeof(CompressedVector3Array), Type.CompressedVector3Array },
			{ typeof(GeometricObjectElementCollideList), Type.GeometricElementCollideList },
			{ typeof(GeometricObjectElementVisualList), Type.GeometricElementVisualList },
			{ typeof(GeometricObject), Type.GeometricObject },
			{ typeof(Vector3Array), Type.Vector3Array },
			{ typeof(Short3Array), Type.Short3Array },
			{ typeof(ObjectsTable), Type.ObjectsTable },
			{ typeof(ObjectsTableData), Type.ObjectsTableData },
			{ typeof(PhysicalCollSet), Type.PhysicalCollSet },
			{ typeof(PhysicalObject), Type.PhysicalObject },
			{ typeof(VertexArray), Type.VertexArray },
			{ typeof(GeometricObjectElementCollideTriangles), Type.GeometricElementCollideTriangles },
			{ typeof(GeometricObjectElementCollideTrianglesData), Type.GeometricELementCollideTrianglesData },
			{ typeof(GeometricObjectElementCollideAlignedBoxes), Type.GeometricElementCollideAlignedBoxes },
			{ typeof(GeometricObjectElementCollideAlignedBoxArray), Type.GeometricElementCollideAlignedBoxArray },
			{ typeof(GeometricObjectElementCollideSpheres), Type.GeometricElementCollideSpheres },
			{ typeof(GeometricObjectElementCollideSphereArray), Type.GeometricElementCollideSphereArray },
			{ typeof(GameMaterial), Type.GameMaterial },
			{ typeof(CollideMaterial), Type.CollideMaterial },
			{ typeof(MechanicsMaterial), Type.MechanicsMaterial },
			{ typeof(LevelList), Type.LevelList },
			{ typeof(SuperObject), Type.SuperObject },
			{ typeof(Sector), Type.Sector },
			{ typeof(SuperObjectArray), Type.SuperObjectArray },
			{ typeof(LevelHeader), Type.LevelHeader },
			{ typeof(HierarchyRoot), Type.HierarchyRoot },
			{ typeof(ShortArray), Type.ShortArray },
			{ typeof(SuperObjectDynamic), Type.SuperObjectDynamic },
			{ typeof(SuperObjectDynamicArray), Type.SuperObjectDynamicArray },
			{ typeof(Perso), Type.Perso },
			{ typeof(Perso3dData), Type.Perso3dData },
			{ typeof(Family), Type.Family },
			{ typeof(State), Type.State },
			{ typeof(AnimationReference), Type.AnimationReference },
			{ typeof(StdGame), Type.StdGame },
			{ typeof(StateArrayRef), Type.StateArrayRef },
			{ typeof(StateArray), Type.StateArray },
			{ typeof(StateTransitionArray), Type.StateTransitionArray },
			{ typeof(SectorSuperObjectArray1), Type.SectorSuperObjectArray1_and_PersoArray },
			{ typeof(SectorSuperObjectArray1Info), Type.SectorSuperObjectArray1Info },
			{ typeof(SectorSuperObjectArray2), Type.SectorSuperObjectArray2 },
			{ typeof(SectorSuperObjectArray3), Type.SectorSuperObjectArray3 },
			{ typeof(SectorSuperObjectArray4), Type.SectorSuperObjectArray4 },
			{ typeof(SectorSuperObjectArray4Info), Type.SectorSuperObjectArray4Info },
			{ typeof(SectorSuperObjectArray5), Type.SectorSuperObjectArray5 },
			{ typeof(SectorSuperObjectArray5Info), Type.SectorSuperObjectArray5Info },
			{ typeof(LightInfoArray), Type.LightInfoArray },
			{ typeof(LightInfo), Type.LightInfo },
			{ typeof(Brain), Type.Brain },
			{ typeof(AIModel), Type.AIModel },
			{ typeof(DsgVar), Type.DsgVar },
			{ typeof(DsgVarInfo), Type.DsgVarInfo },
			{ typeof(DsgVarEntry), Type.DsgVarEntry },
			{ typeof(Comport), Type.Comport },
			{ typeof(Script), Type.Script },
			{ typeof(ScriptArray), Type.ScriptArray },
			{ typeof(ScriptNodeArray), Type.ScriptNodeArray },
			{ typeof(ComportList), Type.ComportList },
			{ typeof(ComportArray), Type.ComportArray },
			{ typeof(WayPoint), Type.WayPoint },
			{ typeof(GraphNode), Type.GraphNode },
			{ typeof(GraphNodeArray), Type.NoCtrlTextureList_and_GraphNodeArray },
			{ typeof(Graph), Type.Graph },
			{ typeof(ArcWeightArray), Type.ArcWeightArray },
			{ typeof(ArcCapsArray), Type.ArcCapsArray },
			{ typeof(CollSet), Type.CollSet },
			{ typeof(ZdxList), Type.ZdxList },
			{ typeof(ActivationList), Type.ActivationList },
			{ typeof(ActivationZoneArray), Type.ActivationZoneArray },
			{ typeof(ActivationZone), Type.ActivationZone },
			{ typeof(ActivationIndexArray), Type.ActivationIndexArray },
			{ typeof(GeometricObjectArray), Type.GeometricObjectArray },
			{ typeof(Scr_Real), Type.Scr_Real },
			{ typeof(Scr_Int), Type.Scr_Int },
			{ typeof(Scr_Vector3), Type.Scr_Vector3 },
			{ typeof(Scr_String), Type.Scr_String },
			{ typeof(Dsg_Real), Type.Dsg_Real },
			{ typeof(Dsg_Int), Type.Dsg_Int },
			{ typeof(Dsg_UInt), Type.Dsg_UInt },
			{ typeof(Dsg_Vector3), Type.Dsg_Vector3 },
			{ typeof(EntryAction), Type.EntryAction },
			{ typeof(KeyWordArray), Type.KeyWordArray },
			{ typeof(EntryActionArray), Type.EntryActionArray },
			{ typeof(InputStruct), Type.InputStruct },
			{ typeof(DsgMem), Type.DsgMem },
			{ typeof(DsgMemInfo), Type.DsgMemInfo },
			{ typeof(DsgMemInfoArray), Type.DsgMemInfoArray },
			{ typeof(PersoArray), Type.SectorSuperObjectArray1_and_PersoArray },
			{ typeof(PersosInFixList), Type.PersosInFixList },
			{ typeof(CameraInfo), Type.Camera },
			{ typeof(PersoMatrixAndVector), Type.PersoMatrixAndVector },
		};

		public Type EntryType {
			get {
				return GetEntryType(type);
			}
		}

		public static Type GetEntryType(ushort type) {
			Dictionary<ushort, Type> dict = null;
			switch (CPA_Settings.s.platform) {
				case CPA_Settings.Platform._3DS: dict = Types3DS; break;
				case CPA_Settings.Platform.N64: dict = TypesN64; break;
				default: dict = TypesDS; break;
			}
			if (dict.ContainsKey(type)) {
				return dict[type];
			} else return Type.Unknown;
		}
	}
}
