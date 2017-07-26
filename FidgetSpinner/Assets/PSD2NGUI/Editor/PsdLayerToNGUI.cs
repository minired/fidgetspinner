using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GBlue;

[InitializeOnLoad]
public class PsdLayerToNGUI : EditorWindow
{
	#region Child Classes
	
	public class Data
	{
		public enum FontType
		{
			None = 0,
			Dynamic,
			Bitmap,
		}
		
		public List<PsdLayerExtractor> extractors = new List<PsdLayerExtractor>();
		public PsdLayerExtractor currentExtractor;
		
		public UIRoot nguiRoot;
		public string nguiRootId;
		public int nguiPanelDepth = 0;
		public int nguiDepth = 0;
		
		public int targetWidth = 720;
		public int targetHeight = 1280;
		
		public FontType fontType = FontType.Dynamic;
		public bool addFont{
			get{
				return 
					(this.fontType == FontType.Bitmap && this.fontData != null && this.fontTexture != null) || 
					(this.fontType == FontType.Dynamic && this.trueTypeFont != null);
			}
		}
		public TextAsset fontData;
		public Texture2D fontTexture;
		public Font trueTypeFont;
		public UIFont fontPrefab;
		
		public bool useOneAtlas = true;
		public UIAtlas atlasPrefab;
		
		public bool keepOldSprites = true;
		public bool createControls = true;
		public bool autoupdate = true;
		
		public Vector2 scrollPosition;
		
		public GameObject nguiRootGameObject
		{
			get{ return this.nguiRoot != null ? this.nguiRoot.gameObject : null; }
		}
		
		public Data ToData()
		{
			var data = new Data();
			data.extractors = new List<PsdLayerExtractor>(this.extractors);
			data.nguiRoot = this.nguiRoot;
			data.nguiRootId = this.nguiRootId;
			data.nguiDepth = this.nguiDepth;
			data.nguiPanelDepth = this.nguiPanelDepth;
			data.targetWidth = this.targetWidth;
			data.targetHeight = this.targetHeight;
			data.fontType = this.fontType;
			data.fontData = this.fontData;
			data.fontTexture = this.fontTexture;
			data.trueTypeFont = this.trueTypeFont;
			data.fontPrefab = this.fontPrefab;
			data.useOneAtlas = this.useOneAtlas;
			data.atlasPrefab = this.atlasPrefab;
			data.keepOldSprites = this.keepOldSprites;
			data.createControls = this.createControls;
			data.autoupdate = this.autoupdate;
			data.scrollPosition = this.scrollPosition;
			return data;
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		
		public override bool Equals(object b)
		{
			try
			{
				var data = b as Data;
				return 
					this.extractors.SequenceEqual(data.extractors) && 
					this.nguiRoot == data.nguiRoot && 
					this.nguiRootId == data.nguiRootId && 
					this.nguiDepth == data.nguiPanelDepth && 
					this.nguiPanelDepth == data.nguiPanelDepth && 
					this.targetWidth == data.targetWidth && 
					this.targetHeight == data.targetHeight && 
					this.fontType == data.fontType && 
					this.fontData == data.fontData && 
					this.fontTexture == data.fontTexture && 
					this.trueTypeFont == data.trueTypeFont && 
					this.fontPrefab == data.fontPrefab && 
					this.useOneAtlas == data.useOneAtlas && 
					this.atlasPrefab == data.atlasPrefab && 
					this.keepOldSprites == data.keepOldSprites && 
					this.createControls == data.createControls && 
					this.autoupdate == data.autoupdate &&
					this.scrollPosition == data.scrollPosition;
			}
			catch (System.Exception)
			{
				return false;
			}
		}
		
		public static bool operator ==(Data a, Data b)
		{
			return a.Equals(b);
		}
		public static bool operator !=(Data a, Data b)
		{
			return !a.Equals(b);
		}
	};
	
	#endregion
	
	#region Properties
	
	public static bool verbose = true;
	private static PsdLayerToNGUI instance;
	
	private static int selectedProjectIndex;
	private static List<string> projectNames = new List<string>();
	private static string projectName = "Default PSD2NGUI Project";

	private static Texture2D logo;
	private static bool actionFold = true;
	private static bool settingFold = true;
	private static bool psdFold = true;
	private static Timer loadTimer;
	
	private static Data data = new Data();

	private static string CurrentPath
	{
		get
		{
			var assetPath = PsdLayerToNGUI.data.extractors.Count > 0 ? 
				PsdLayerToNGUI.data.extractors[0].PsdFilePath : AssetDatabase.GetAssetPath(Selection.activeObject);
			
			if (assetPath.Length > 0)
			{
				var i = assetPath.LastIndexOfAny("/".ToCharArray());
				if (i >= 0)
				{
					assetPath = assetPath.Remove(i + 1);
					return assetPath;
				}
			}
			return null;
		}
	}
	
	#endregion

	void OnEnable()
	{
		PsdLayerToNGUI.loadTimer = new Timer();
		PsdLayerToNGUI.logo = Resources.Load("logo") as Texture2D;
	}
	
	void OnDestroy()
	{
	}
	
	void Update()
	{
		if (Application.isPlaying)
			return;
		
		// load saved file
		
		if (PsdLayerToNGUI.loadTimer != null)
		{
			if (!PsdLayerToNGUI.loadTimer.IsPlaying)
			{
				PsdLayerToNGUI.loadTimer.Play(null, 0.5f, false, true, delegate(){
					PsdLayerToNGUI.loadTimer = null;
					PsdLayerToNGUI.Load();
					this.Repaint();
				});
			}
			PsdLayerToNGUI.loadTimer.Update();
		}
		
		// check psd file modification
		
		if (PsdLayerToNGUI.data.autoupdate)
			PsdLayerToNGUI.UpdatePsdFile();
		
		// update UIRoot, Psd GameObject location, etc
		
		for (var i=0; i<PsdLayerToNGUI.data.extractors.Count; ++i)
		{
			var extractor = PsdLayerToNGUI.data.extractors[i];
			extractor.Update(PsdLayerToNGUI.data.nguiRootGameObject);
		}
	}
	
	private void DrawLogo(float x, float y, float size)
	{
		if (PsdLayerToNGUI.logo != null)
		{
			var rc = new Rect(x, y, size, size * 0.5f);
			GUI.DrawTexture(rc, PsdLayerToNGUI.logo, ScaleMode.StretchToFill, true);
		}
	}
	private void DrawLogo(float sectionWidth)
	{
		var rc = EditorGUILayout.BeginVertical();
		{
			GUILayout.Space(32);
			var logoSize = 128;
			this.DrawLogo(sectionWidth - logoSize, rc.y, logoSize);
		}
		EditorGUILayout.EndVertical();
		GUILayout.Space(5);
	}

	private void DrawActionSection(float sectionWidth, float minWidth)
	{
		// load, save

		GUILayout.BeginHorizontal();
		{
			var i = PsdLayerToNGUI.selectedProjectIndex;
			i = EditorGUILayout.Popup(i, PsdLayerToNGUI.projectNames.ToArray(), GUILayout.MaxWidth(200));
			PsdLayerToNGUI.selectedProjectIndex = i;
			
			var validSelection = i >= 0 && i < PsdLayerToNGUI.projectNames.Count;
			var name = validSelection ? PsdLayerToNGUI.projectNames[i] : "";
			GUI.enabled = validSelection;
			{
				if (GUILayout.Button("Load", GUILayout.MaxWidth(80)) && !string.IsNullOrEmpty(name))
				{
					PsdLayerToNGUI.projectName = name;
					PsdLayerToNGUI.Load(PsdLayerToNGUI.projectName);
				}
				if (GUILayout.Button("Delete", GUILayout.MaxWidth(80)) && !string.IsNullOrEmpty(name))
				{
					PsdLayerToNGUI.Delete(name);
				}
			}
			GUI.enabled = true;
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		{
			PsdLayerToNGUI.projectName = 
				EditorGUILayout.TextField(PsdLayerToNGUI.projectName, GUILayout.MaxWidth(200));
			
			if (GUILayout.Button("Save", GUILayout.MaxWidth(80)))
			{
				if (string.IsNullOrEmpty(PsdLayerToNGUI.projectName))
					Debug.LogError("PSD2NGUI Project name is wrong.");
				else
				{
					if (!PsdLayerToNGUI.projectNames.Contains(PsdLayerToNGUI.projectName))
						PsdLayerToNGUI.projectNames.Add(PsdLayerToNGUI.projectName);
					
					PsdLayerToNGUI.selectedProjectIndex = PsdLayerToNGUI.projectNames.Count - 1;
					PsdLayerToNGUI.Save();
				}
			}
			if (GUILayout.Button("New", GUILayout.MaxWidth(80)))
			{
				PsdLayerToNGUI.projectName = "New PSD2NGUI Project";
				PsdLayerToNGUI.data.nguiRoot = null;
				PsdLayerToNGUI.data.extractors.Clear();
			}
		}
		GUILayout.EndHorizontal();

		// run
		
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Run", new GUILayoutOption[]{GUILayout.MaxWidth(368), GUILayout.MaxHeight(40)}))
		{
			if (Application.isPlaying)
				return;
			
			if (PsdLayerToNGUI.data.extractors.Count > 0)
			{
				Save();
				this.OnRun();
				Save();
			}
			else
				EditorUtility.DisplayDialog("Notice", "No PSD file(s) added", "Ok");
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
	}
	
	private void DrawRootSettingSection(float sectionWidth, float minWidth)
	{
		// root
		
		GUILayout.BeginHorizontal();
		{
			PsdLayerToNGUI.data.nguiRoot = EditorGUILayout.ObjectField(
				"NGUI Root", PsdLayerToNGUI.data.nguiRoot, typeof(UIRoot), true, GUILayout.MaxWidth(360)) as UIRoot;
			
			if (PsdLayerToNGUI.data.nguiRoot != null)
			{
				var guid = Util.FindComponent<PsdLayerGuid>(PsdLayerToNGUI.data.nguiRoot);
				if (guid != null)
				{
					PsdLayerToNGUI.data.nguiRootId = guid.guid;
					PsdLayerToNGUI.Save();
				}
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
	}
	
	private void DrawCameraSettingSection(float sectionWidth, float minWidth)
	{
		// camera & pivot
		
		GUILayout.BeginHorizontal();
		{
			NGUIRootCreator.camType = (UICreateNewUIWizard.CameraType)EditorGUILayout.EnumPopup(
				"Camera", NGUIRootCreator.camType, GUILayout.MaxWidth(300));
			
			//**todo
//			pivot = (Pivot)EditorGUILayout.EnumPopup(
//				"Pivot for UI", pivot, GUILayout.MaxWidth(220));
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
		
		// resolution
		
		GUILayout.BeginHorizontal();
		{
			EditorGUILayout.LabelField("Target Screen Size", GUILayout.MaxWidth(147));
			PsdLayerToNGUI.data.targetWidth = EditorGUILayout.IntField(
				PsdLayerToNGUI.data.targetWidth, GUILayout.MaxWidth(50));
			
			EditorGUILayout.LabelField("x", GUILayout.MaxWidth(10));
			PsdLayerToNGUI.data.targetHeight = EditorGUILayout.IntField(
				PsdLayerToNGUI.data.targetHeight, GUILayout.MaxWidth(50));
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
	}

	private void DrawFontSettingSection(float sectionWidth, float minWidth)
	{
		// font
		
		GUILayout.BeginHorizontal();
		{
			PsdLayerToNGUI.data.fontType = (PsdLayerToNGUI.Data.FontType)EditorGUILayout.EnumPopup(
				"Font Type", PsdLayerToNGUI.data.fontType, GUILayout.MaxWidth(300));
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
		
		if (PsdLayerToNGUI.data.fontType == Data.FontType.Bitmap)
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("", GUILayout.MaxWidth(5));
				GUILayout.Label("Bitmap font which included in an atlas will not support for", GUILayout.MaxWidth(350));
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(1);
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("", GUILayout.MaxWidth(5));
				GUILayout.Label("NGUI v3.0.4 ~ v3.5.8", GUILayout.MaxWidth(250));
			}
			GUILayout.EndHorizontal();

			//**NGUI problem
//			GUILayout.BeginHorizontal();
//			{
//				GUILayout.Label("", GUILayout.MaxWidth(5));
//				PsdLayerToNGUI.data.fontData = EditorGUILayout.ObjectField(
//					"Font Data", PsdLayerToNGUI.data.fontData, typeof(TextAsset), false, GUILayout.MaxWidth(300)) as TextAsset;
//			}
//			GUILayout.EndHorizontal();
//			GUILayout.Space(1);
//				
//			GUILayout.BeginHorizontal();
//			{
//				GUILayout.Label("", GUILayout.MaxWidth(5));
//				PsdLayerToNGUI.data.fontTexture = EditorGUILayout.ObjectField(
//					"Font Texture", PsdLayerToNGUI.data.fontTexture, typeof(Texture2D), false, GUILayout.MaxWidth(300)) as Texture2D;
//			}
//			GUILayout.EndHorizontal();
//			GUILayout.Space(1);
//			
//			GUILayout.BeginHorizontal();
//			{
//				GUILayout.Label("", GUILayout.MaxWidth(5));
//				PsdLayerToNGUI.data.fontPrefab = EditorGUILayout.ObjectField(
//					"Font Prefab", PsdLayerToNGUI.data.fontPrefab, typeof(UIFont), false, GUILayout.MaxWidth(300)) as UIFont;
//				
//				if (GUILayout.Button("Assign Font To All Labels", GUILayout.MaxWidth(150)))
//				{
//					var labels = GameObject.FindObjectsOfType(typeof(UILabel)) as UILabel[];
//					foreach (var label in labels)
//					{
//						if (label.ambigiousFont == null)
//							label.ambigiousFont = PsdLayerToNGUI.data.fontPrefab;
//					}
//				}
//			}
//			GUILayout.EndHorizontal();
		}
		else if (PsdLayerToNGUI.data.fontType == Data.FontType.Dynamic)
		{
#if UNITY_3_5
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("", GUILayout.MaxWidth(5));
				GUILayout.Label("Dynamic fonts require Unity 4.0 or higher", GUILayout.MaxWidth(250));
			}
			GUILayout.EndHorizontal();
#else
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("", GUILayout.MaxWidth(5));
				PsdLayerToNGUI.data.trueTypeFont = EditorGUILayout.ObjectField(
					"True Type Font", PsdLayerToNGUI.data.trueTypeFont, 
					typeof(Font), false, GUILayout.MaxWidth(300)
				) as Font;
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(1);
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("", GUILayout.MaxWidth(5));
				GUILayout.Label("Multiple NGUI dynamic fonts will be generated", GUILayout.MaxWidth(250));
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(1);
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("", GUILayout.MaxWidth(5));
				GUILayout.Label("according to the size of PhotoShop Text Layers", GUILayout.MaxWidth(250));
			}
			GUILayout.EndHorizontal();
#endif
		}
		else
		{
			GUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Remove Font From All Labels", GUILayout.MaxWidth(180)))
				{
					var labels = GameObject.FindObjectsOfType(typeof(UILabel)) as UILabel[];
					foreach (var label in labels){
						label.bitmapFont = null;
						label.trueTypeFont = null;
					}
				}
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.Space(1);
	}

	private void DrawEtcSettingSection(float sectionWidth, float minWidth)
	{
		// one or multiple atlas
		
		GUILayout.BeginHorizontal();
		{
			PsdLayerToNGUI.data.useOneAtlas = GUILayout.Toggle(
				PsdLayerToNGUI.data.useOneAtlas, "Use One Atlas", GUILayout.MaxWidth(130));
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
		
		if (PsdLayerToNGUI.data.useOneAtlas)
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("", GUILayout.MaxWidth(5));
				
				PsdLayerToNGUI.data.keepOldSprites = GUILayout.Toggle(
					PsdLayerToNGUI.data.keepOldSprites, "Keep Old Sprites", GUILayout.MaxWidth(120));
				
				PsdLayerToNGUI.data.atlasPrefab = EditorGUILayout.ObjectField(
					PsdLayerToNGUI.data.atlasPrefab, typeof(UIAtlas), false, GUILayout.MaxWidth(200)) as UIAtlas;
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(1);
		}
		
		// create controls
		
		GUILayout.BeginHorizontal();
		{
			PsdLayerToNGUI.data.createControls = GUILayout.Toggle(
				PsdLayerToNGUI.data.createControls, "Create Controls", GUILayout.MaxWidth(130));
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
		
		// update psd automatically
		
		GUILayout.BeginHorizontal();
		{
			PsdLayerToNGUI.data.autoupdate = GUILayout.Toggle(
				PsdLayerToNGUI.data.autoupdate, "Update when files modified", GUILayout.MaxWidth(200));
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
		
		// All or None buttons
		
		GUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Remove All", GUILayout.MaxWidth(80)))
			{
				var body = "Are you sure you want to remove all PSD files?";
				if (EditorUtility.DisplayDialog("Alert", body, "Yes", "No"))
					PsdLayerToNGUI.data.extractors.Clear();
			}
			
			if (!PsdLayerToNGUI.data.useOneAtlas && PsdLayerToNGUI.data.addFont)
			{
				if (GUILayout.Button("Add Font All", GUILayout.MaxWidth(100)))
				{
					for (var i=0; i<PsdLayerToNGUI.data.extractors.Count; ++i)
						PsdLayerToNGUI.data.extractors[i].IsAddFont = true;
				}
				
				if (GUILayout.Button("Add Font None", GUILayout.MaxWidth(100)))
				{
					for (var i=0; i<PsdLayerToNGUI.data.extractors.Count; ++i)
						PsdLayerToNGUI.data.extractors[i].IsAddFont = false;
				}
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(1);
	}
	
	private void DrawPsdSection(float sectionWidth, float minWidth)
	{
		// layers
		
		GUILayout.Space(10);
		for (var i=0; i<PsdLayerToNGUI.data.extractors.Count; ++i)
		{
			GUILayout.BeginVertical();
			{
				var isAddFontToggleVisible = (!PsdLayerToNGUI.data.useOneAtlas && PsdLayerToNGUI.data.addFont);
				PsdLayerToNGUI.data.extractors[i].OnGUI(isAddFontToggleVisible, delegate(){
					PsdLayerToNGUI.data.extractors.RemoveAt(i--);
				});
			}
			GUILayout.EndVertical();
		}
		GUILayout.Space(10);

		EditorGUILayout.BeginVertical("HelpBox", GUILayout.Width(sectionWidth), GUILayout.MinWidth(minWidth));
		{
			EditorGUILayout.LabelField("Add PSD file");
			
			Texture2D psd = null;
			psd = EditorGUILayout.ObjectField("", 
				psd, typeof(Texture2D), false, GUILayout.MaxWidth(100)
			) as Texture2D;
			
			if (psd != null)
			{
				if (Selection.objects.Length > 1)
				{
				    for (var i=0; i<Selection.objects.Length; ++i)
				    {
				        var obj = Selection.objects[i];
				        var filePath = AssetDatabase.GetAssetPath(obj);
						if (filePath.EndsWith(".psd", System.StringComparison.CurrentCultureIgnoreCase))
						{
							if (PsdLayerToNGUI.data.extractors.Find(x => x.PsdFilePath == filePath) == null)
							{
								PsdLayerToNGUI.data.extractors.Add(
									new PsdLayerExtractor(PsdLayerToNGUI.data.nguiRootGameObject, obj, 
										filePath, PsdLayerToNGUI.AddPsdFileToUpdate)
								);
							}
							else
								Debug.LogWarning(filePath + " is already exist");
						}
				    }
				}
				else
				{
					var filePath = AssetDatabase.GetAssetPath(psd);
					if (PsdLayerToNGUI.data.extractors.Find(x => x.PsdFilePath == filePath) == null)
					{
						PsdLayerToNGUI.data.extractors.Add(
							new PsdLayerExtractor(PsdLayerToNGUI.data.nguiRootGameObject, psd,
								filePath, PsdLayerToNGUI.AddPsdFileToUpdate)
						);
					}
					else
						Debug.LogWarning(filePath + " is already exist");
				}
			}
		}
		EditorGUILayout.EndVertical();
	}

	private void DrawSettingSection(float sectionWidth, float minWidth)
	{
		this.DrawSection("", sectionWidth, minWidth, this.DrawRootSettingSection);
		this.DrawSection("", sectionWidth, minWidth, this.DrawCameraSettingSection);
		this.DrawSection("", sectionWidth, minWidth, this.DrawFontSettingSection);
		this.DrawSection("", sectionWidth, minWidth, this.DrawEtcSettingSection);
	}

	private void DrawSection(string title, float sectionWidth, float minWidth, System.Action<float, float> func)
	{
		if (!string.IsNullOrEmpty(title))
			EditorGUILayout.LabelField(title, GUILayout.MaxWidth(100));

		EditorGUILayout.BeginVertical("HelpBox", GUILayout.Width(sectionWidth), GUILayout.MinWidth(minWidth));
		GUILayout.Space(10);
		func(sectionWidth, minWidth);
		GUILayout.Space(10);
		EditorGUILayout.EndVertical();
	}
	
	private void DrawSection(ref bool fold, string title, 
		float sectionWidth, float minWidth, System.Action<float, float> func)
	{
		if (!string.IsNullOrEmpty(title))
			fold = EditorGUILayout.Foldout(fold, title);

		if (fold)
		{
			EditorGUILayout.BeginVertical("HelpBox", GUILayout.Width(sectionWidth), GUILayout.MinWidth(minWidth));
			GUILayout.Space(1);
			func(sectionWidth, minWidth);
			GUILayout.Space(1);
			EditorGUILayout.EndVertical();
		}
	}

	void OnGUI()
	{
		if (Application.isPlaying)
		{
			EditorGUILayout.LabelField("Can not modify on a playing mode", GUILayout.MaxWidth(200));
			this.Repaint();
			return;
		}
		
		GUILayout.BeginArea(new Rect(0,0, base.position.width, base.position.height));
		PsdLayerToNGUI.data.scrollPosition = EditorGUILayout.BeginScrollView(PsdLayerToNGUI.data.scrollPosition, 
			false, false,GUILayout.MinWidth(300),GUILayout.MaxWidth(base.position.width)
		);

		var rightPad = 23;
		var minWidth = 375;
		var sectionWidth = base.position.width - rightPad;

		this.DrawLogo(sectionWidth);
		this.DrawSection(ref PsdLayerToNGUI.actionFold, "ACTION", sectionWidth, minWidth, this.DrawActionSection);
		this.DrawSection(ref PsdLayerToNGUI.settingFold, "SETTING", sectionWidth, minWidth, this.DrawSettingSection);
		this.DrawSection(ref PsdLayerToNGUI.psdFold, "PSD FILES", sectionWidth, minWidth, this.DrawPsdSection);

		EditorGUILayout.EndScrollView();
		GUILayout.EndArea();
	}
	
	private void OnRun()
	{
		if (PsdLayerToNGUI.data.useOneAtlas)
		{
			var textures = new List<Texture>();
			var newPsdCount = 0;
			foreach (var extractor in PsdLayerToNGUI.data.extractors)
			{
				extractor.Update(PsdLayerToNGUI.data.nguiRootGameObject);
				PsdLayerToNGUI.data.currentExtractor = extractor;
				
				if (!extractor.IsLinked)
				{
					PsdLayerToNGUI.ExtractTextures(textures, extractor);
					newPsdCount++;
				}
				else if (extractor.IsLinked && extractor.IsChanged)
				{
					PsdLayerToNGUI.ExtractTextures(textures, extractor);
				}
			}
			
			if (textures.Count > 0)
			{
				var firstext = PsdLayerToNGUI.data.extractors[0];
				PsdLayerToNGUI.data.currentExtractor = firstext;
				
				var createAtlas = newPsdCount == PsdLayerToNGUI.data.extractors.Count;
				if (createAtlas)
					PsdLayerToNGUI.CreateNGUIAtlas(firstext, textures);
				else
					PsdLayerToNGUI.UpdateNGUIAtlas(firstext, textures);
				
				foreach (var extractor in PsdLayerToNGUI.data.extractors)
				{
					if (!extractor.IsLinked)
						PsdLayerToNGUI.CreateNGUIs(extractor);
					
					else if (extractor.IsLinked && extractor.IsChanged)
						PsdLayerToNGUI.UpdateNGUIs(extractor);
				}
			}
		}
		else
		{
			foreach (var extractor in PsdLayerToNGUI.data.extractors)
			{
				extractor.Update(PsdLayerToNGUI.data.nguiRootGameObject);
				PsdLayerToNGUI.data.currentExtractor = extractor;
				
				var texes = PsdLayerToNGUI.ExtractTextures(extractor);
				if (texes != null)
				{
					if (!extractor.IsLinked)
					{
						PsdLayerToNGUI.CreateNGUIAtlas(extractor, texes);
						PsdLayerToNGUI.CreateNGUIs(extractor);
					}
					else if (extractor.IsLinked && extractor.IsChanged)
					{
						PsdLayerToNGUI.UpdateNGUIAtlas(extractor, texes);
						PsdLayerToNGUI.UpdateNGUIs(extractor);
					}
				}
			}
		}
	}
	
	[MenuItem("NGUI/Open PSD Layers to NGUI", false, 20003)]
	public static void CreateUIWizard()
	{
		if (PsdLayerToNGUI.instance == null)
			PsdLayerToNGUI.instance = EditorWindow.GetWindow<PsdLayerToNGUI>(false, "PSD2NGUI", true);
		PsdLayerToNGUI.instance.Show();
		PsdLayerToNGUI.instance.Focus();
	}
	
	[MenuItem ("Assets/Save PSD Layers to NGUI", true, 20002)]
	public static bool SaveLayersEnabled()
	{
	    for (var i=0; i<Selection.objects.Length; ++i)
	    {
	        var obj = Selection.objects[i];
	        var filePath = AssetDatabase.GetAssetPath(obj);
			if (filePath.EndsWith(".psd", System.StringComparison.CurrentCultureIgnoreCase))
				return true;
	    }
		
		return false;
	}
	
	[MenuItem ("Assets/Save PSD Layers to NGUI", false, 20002)]
	public static void SaveLayers()
	{
		if (PsdLayerToNGUI.instance == null)
			PsdLayerToNGUI.instance = EditorWindow.GetWindow<PsdLayerToNGUI>(false, "PSD2NGUI", true);
		PsdLayerToNGUI.instance.Show();
		PsdLayerToNGUI.instance.Focus();
		PsdLayerToNGUI.LoadFromSelection();
	}
	
	private static void Load()
	{
		PsdLayerToNGUI.selectedProjectIndex = PsdLayerPrefs.GetInt("PsdParser.selectedProjectIndex", -1);
		PsdLayerToNGUI.projectName = PsdLayerPrefs.GetString("PsdParser.projectName", PsdLayerToNGUI.projectName);
		PsdLayerToNGUI.projectNames.Clear();
		PsdLayerToNGUI.projectNames.AddRange(PsdLayerPrefs.GetStringArray("PsdParser.projectNames"));
		PsdLayerToNGUI.Load(PsdLayerToNGUI.projectName);
	}
	private static void Load(string projectName)
	{
		var preName = "PsdParser." + projectName + ".";
		
		var nguiRootId = PsdLayerPrefs.GetString(preName + "nguiRoot");
		{
			var nguiRootIds = GameObject.FindObjectsOfType(typeof(PsdLayerGuid)) as PsdLayerGuid[];
			for (var i=0; i<nguiRootIds.Length; ++i)
			{
				if (nguiRootIds[i].guid == nguiRootId)
				{
					PsdLayerToNGUI.data.nguiRoot = Util.FindComponent<UIRoot>(nguiRootIds[i].gameObject);
					PsdLayerToNGUI.data.nguiRootId = nguiRootIds[i].guid;
					break;
				}
			}
		}
		PsdLayerToNGUI.data.targetWidth = PsdLayerPrefs.GetInt(preName + "targetWidth", 720);
		PsdLayerToNGUI.data.targetHeight = PsdLayerPrefs.GetInt(preName + "targetHeight", 1280);
		NGUIRootCreator.camType = (UICreateNewUIWizard.CameraType)PsdLayerPrefs.GetInt(preName + "camType", 2);
		
		PsdLayerToNGUI.data.fontType = (PsdLayerToNGUI.Data.FontType)PsdLayerPrefs.GetInt(preName + "fontType", 1);
		PsdLayerToNGUI.data.useOneAtlas = PsdLayerPrefs.GetInt(preName + "useOneAtlas", 1) == 1;
		PsdLayerToNGUI.data.keepOldSprites = PsdLayerPrefs.GetInt(preName + "keepOldSprites", 1) == 1;
		PsdLayerToNGUI.data.createControls = PsdLayerPrefs.GetInt(preName + "createControls", 1) == 1;
		PsdLayerToNGUI.data.autoupdate = PsdLayerPrefs.GetInt(preName + "autoupdate", 1) == 1;
		
		var atlasPrefab = AssetDatabase.LoadAssetAtPath(
			PsdLayerPrefs.GetString(preName + "atlasPrefab"), typeof(GameObject)) as GameObject;
		if (atlasPrefab != null)
			PsdLayerToNGUI.data.atlasPrefab = atlasPrefab.GetComponent<UIAtlas>();
		
		var fontPrefab = AssetDatabase.LoadAssetAtPath(
			PsdLayerPrefs.GetString(preName + "fontPrefab"), typeof(GameObject)) as GameObject;
		if (fontPrefab != null)
			PsdLayerToNGUI.data.fontPrefab = fontPrefab.GetComponent<UIFont>();
		
		PsdLayerToNGUI.data.fontData = AssetDatabase.LoadAssetAtPath(
			PsdLayerPrefs.GetString(preName + "fontData"), typeof(TextAsset)) as TextAsset;
		
		PsdLayerToNGUI.data.fontTexture = AssetDatabase.LoadAssetAtPath(
			PsdLayerPrefs.GetString(preName + "fontTexture"), typeof(Texture2D)) as Texture2D;
		
		PsdLayerToNGUI.data.trueTypeFont = AssetDatabase.LoadAssetAtPath(
			PsdLayerPrefs.GetString(preName + "trueTypeFont"), typeof(Font)) as Font;
		
		var psdFileInfos = PsdLayerPrefs.GetStringArray(preName + "psdFiles");
		PsdLayerToNGUI.data.extractors.Clear();
		foreach (var info in psdFileInfos)
		{
			if (info.Length > 1)
			{
				try
				{
					PsdLayerToNGUI.data.extractors.Add(
						new PsdLayerExtractor(PsdLayerToNGUI.data.nguiRootGameObject, null, 
							info, PsdLayerToNGUI.AddPsdFileToUpdate)
					);
				}
				catch (System.Exception e)
				{
					Debug.LogError(e.Message);
				}
			}
		}
	}
	
	private static void Delete(string projectName)
	{
		var preName = "PsdParser." + projectName + ".";
		
		PsdLayerPrefs.DeleteKey(preName + "nguiRoot");
		PsdLayerPrefs.DeleteKey(preName + "targetWidth");
		PsdLayerPrefs.DeleteKey(preName + "targetHeight");
		
		PsdLayerPrefs.DeleteKey(preName + "fontType");
		PsdLayerPrefs.DeleteKey(preName + "useOneAtlas");
		PsdLayerPrefs.DeleteKey(preName + "keepOldSprites");
		PsdLayerPrefs.DeleteKey(preName + "createControls");
		PsdLayerPrefs.DeleteKey(preName + "autoupdate");
		
		PsdLayerPrefs.DeleteKey(preName + "atlasPrefab");
		PsdLayerPrefs.DeleteKey(preName + "fontPrefab");
		PsdLayerPrefs.DeleteKey(preName + "fontData");
		PsdLayerPrefs.DeleteKey(preName + "fontTexture");
		PsdLayerPrefs.DeleteKey(preName + "trueTypeFont");
		PsdLayerPrefs.DeleteKey(preName + "psdFiles");
		
		PsdLayerToNGUI.projectNames.Remove(projectName);
	}
	
	private static void LoadFromSelection()
	{
	    for (var i=0; i<Selection.objects.Length; ++i)
	    {
	        var obj = Selection.objects[i];
	        var filePath = AssetDatabase.GetAssetPath(obj);
			if (!filePath.EndsWith(".psd", System.StringComparison.CurrentCultureIgnoreCase))
				continue;
			
			if (PsdLayerToNGUI.data.extractors.Find(x => x.PsdFilePath == filePath) == null)
			{
				PsdLayerToNGUI.data.extractors.Add(
					new PsdLayerExtractor(PsdLayerToNGUI.data.nguiRootGameObject, null, 
						filePath, PsdLayerToNGUI.AddPsdFileToUpdate)
				);
			}
			else
				Debug.LogWarning(filePath + " is already exist");
	    }
	}
	
	private static void Save()
	{
		if (PsdLayerToNGUI.instance != null && PsdLayerToNGUI.loadTimer != null)
			return;
		
		PsdLayerPrefs.SetInt("PsdParser.selectedProjectIndex", PsdLayerToNGUI.selectedProjectIndex);
		PsdLayerPrefs.SetString("PsdParser.projectName", PsdLayerToNGUI.projectName);
		PsdLayerPrefs.SetStringArray("PsdParser.projectNames", PsdLayerToNGUI.projectNames.ToArray());
		PsdLayerToNGUI.Save(PsdLayerToNGUI.projectName);
	}
	private static void Save(string projectName)
	{
		var preName = "PsdParser." + projectName + ".";
		
		PsdLayerPrefs.SetString(preName + "nguiRoot", PsdLayerToNGUI.data.nguiRootId);
		PsdLayerPrefs.SetInt(preName + "targetWidth", PsdLayerToNGUI.data.targetWidth);
		PsdLayerPrefs.SetInt(preName + "targetHeight", PsdLayerToNGUI.data.targetHeight);
		PsdLayerPrefs.SetInt(preName + "camType", (int)NGUIRootCreator.camType);
		
		PsdLayerPrefs.SetInt(preName + "fontType", (int)PsdLayerToNGUI.data.fontType);
		PsdLayerPrefs.SetInt(preName + "useOneAtlas", PsdLayerToNGUI.data.useOneAtlas ? 1 : 0);
		PsdLayerPrefs.SetInt(preName + "keepOldSprites", PsdLayerToNGUI.data.keepOldSprites ? 1 : 0);
		PsdLayerPrefs.SetInt(preName + "createControls", PsdLayerToNGUI.data.createControls ? 1 : 0);
		PsdLayerPrefs.SetInt(preName + "autoupdate", PsdLayerToNGUI.data.autoupdate ? 1 : 0);
		
		PsdLayerPrefs.SetString(preName + "atlasPrefab", AssetDatabase.GetAssetPath(PsdLayerToNGUI.data.atlasPrefab));
		PsdLayerPrefs.SetString(preName + "fontPrefab", AssetDatabase.GetAssetPath(PsdLayerToNGUI.data.fontPrefab));
		PsdLayerPrefs.SetString(preName + "fontData", AssetDatabase.GetAssetPath(PsdLayerToNGUI.data.fontData));
		PsdLayerPrefs.SetString(preName + "fontTexture", AssetDatabase.GetAssetPath(PsdLayerToNGUI.data.fontTexture));
		PsdLayerPrefs.SetString(preName + "trueTypeFont", AssetDatabase.GetAssetPath(PsdLayerToNGUI.data.trueTypeFont));
		
		var psdFileInfos = new List<string>();
		for (var i=0; i<PsdLayerToNGUI.data.extractors.Count; ++i)
		{
			var extractor = PsdLayerToNGUI.data.extractors[i];
			psdFileInfos.Add(extractor.ToString());
		}
		PsdLayerPrefs.SetStringArray(preName + "psdFiles", psdFileInfos.ToArray());
	}
	
	private static PsdLayerCommandParser.Control[] FindControlSources(
		PsdLayerCommandParser.Control c, string controlName, string[] sourceNames)
	{
		var controlNames = controlName.Split(',');
		var sources = new PsdLayerCommandParser.Control[sourceNames.Length];
		for (var i=0; i<c.sources.Count; ++i)
		{
			var src = c.sources[i];
			SetSlicedSprite(src.fullName, src.sliceArea);
			
			var first = false;
			foreach (var name in controlNames)
			{
				if (first = src.command.EndsWith(name))
					break;
			}
			
			if (first || src.command.EndsWith(sourceNames[0]))
			{
				sources[0] = src;
			}
			else
			{
				for (var n=1; n<sources.Length; ++n)
				{
					if (src.command.EndsWith(sourceNames[n]))
						sources[n] = src;
				}
			}
		}
		return sources;
	}
	
	private static PsdLayerCommandParser.Control FindDynamicFontContainer(
		PsdLayerCommandParser.Control c)
	{
		if (c.fontSize > 0)
			return c;
		
		for (var i=0; i<c.sources.Count; ++i)
		{
			var src = c.sources[i];
			if (src.fontSize > 0)
				return src;
		}
		return null;
	}
	
	private static bool SetSlicedSprite(string fullName, PsdLayerRect sliceArea)
	{
		if (sliceArea == null)
			return false;
		
		var spr = PsdLayerToNGUI.data.atlasPrefab.GetSprite(fullName);
		if (spr != null)
		{
			spr.borderLeft = (int)sliceArea.left;
			spr.borderRight = (int)sliceArea.right;
			spr.borderBottom = (int)sliceArea.bottom;
			spr.borderTop = (int)sliceArea.top;
		}
		return true;
	}
	
	private static void SetButtonAppearance(
		GameObject go, PsdLayerCommandParser.Control c, UILabel label, bool enable)
	{
		var btn = Util.FindComponent<UIButton>(go);
		var btnScale = Util.FindComponent<UIButtonScale>(go);
		var btnOffset = Util.FindComponent<UIButtonOffset>(go);
		UIWidget btnBackground = null;
		if (btn != null)
		{
			btn.enabled = enable;
			btn.hover = Color.yellow;
			btn.pressed = new Color(.7f, .7f, .7f, 1f);
			
			btnBackground = Util.FindComponent<UIWidget>(btn.tweenTarget);
			
			if (c.type == PsdLayerCommandParser.ControlType.LabelButton){
				if (btnBackground != null){
					btnBackground.alpha = 0;
				}
				btn.tweenTarget = label.gameObject;
				btnBackground = Util.FindComponent<UIWidget>(btn.tweenTarget);
			}
		}
		if (btnScale != null)
		{
			btnScale.hover = Vector3.one;
			btnScale.pressed = new Vector3(0.998f, 0.998f, 1);
		}
		if (btnOffset != null)
		{
			btnOffset.pressed = Vector3.zero;
		}
		
		var imgbtn = Util.FindComponent<UIImageButton>(go);
		if (imgbtn != null)
		{
			imgbtn.target.spriteName = imgbtn.normalSprite;
		}
		
		var toggle = Util.FindComponent<UIToggle>(go);
		if (toggle != null && c.sources.Count == 1)
		{
			btnBackground.color = new Color(.7f, .7f, .7f, 1f);
		}
	}
	
	private static void SetLabelAppearance(UILabel label, PsdLayerCommandParser.Control c)
	{
		PsdLayerToNGUI.SetLabelAppearance(label, c, Vector3.zero);
	}
	private static void SetLabelAppearance(UILabel label, PsdLayerCommandParser.Control c, Vector3 labelSize)
	{
		if (label == null)
			return;
		
		if (c != null)
		{
			// try to create dynamic font
			
			var extractor = PsdLayerToNGUI.data.currentExtractor;
			if (PsdLayerToNGUI.data.fontType == Data.FontType.Dynamic && extractor != null)
			{
				if (c.fontSize > 0)
				{
					var currentPath = PsdLayerToNGUI.CurrentPath;
					var name = extractor.PsdFileName.Substring(0, extractor.PsdFileName.Length - 4);
					NGUIFontCreator.CreateDynamic(currentPath + name, PsdLayerToNGUI.data, c);
				}
			}
			
			label.ambigiousFont = NGUISettings.ambigiousFont;
			
			if (c.bold)
			{
				label.effectStyle = UILabel.Effect.Outline;
				label.effectColor = new Color(c.color.r, c.color.g, c.color.b, 0.8f);
			}
			
			label.text = c.text;
			label.color = c.color;
			label.height = Mathf.RoundToInt(c.area.height);
			label.fontSize = c.fontSize;
			
			if (labelSize != Vector3.zero){
				label.width = Mathf.RoundToInt(labelSize.x * 0.98f);
			}
			else{
				label.width = Mathf.RoundToInt(c.area.width);
			}
		}
		else
		{
			label.text = "";
			label.color = Color.black;
		}
	}
	
	private static void MoveAndResize(GameObject go, PsdLayerCommandParser.Control c)
	{
		// set depth and init
		
		var widgets = Util.FindComponents<UIWidget>(go);
		if (widgets != null)
		{
			foreach (var widget in widgets)
			{
				widget.depth = PsdLayerToNGUI.data.nguiDepth++;
				widget.pivot = UIWidget.Pivot.Center;
				widget.cachedTransform.localPosition = Vector3.zero;
			}
		}
		
		// set size
		
		var thisWidget = go.GetComponent<UIWidget>();
		var w = (float)c.area.width;
		var h = (float)c.area.height;
		{
			var sprs = Util.FindComponents<UISprite>(go);
			if (sprs != null)
			{
				foreach (var spr in sprs)
				{
					spr.width = Mathf.RoundToInt(w);
					spr.height = Mathf.RoundToInt(h);
				}
			}
			
			if (c.type != PsdLayerCommandParser.ControlType.VirtualView)
			{
				var collider = Util.FindComponent<BoxCollider>(go);
				if (collider != null)
				{
					var z = 0f;
					if (NGUIRootCreator.camType == UICreateNewUIWizard.CameraType.Simple2D)
						z = collider.center.z;
					
					collider.center = new Vector3(0, 0, Mathf.RoundToInt(z));
					collider.size = new Vector3(
						Mathf.RoundToInt(w), 
						Mathf.RoundToInt(h), 1);
				}
			}
			
			if (c.hasBoxCollider)
			{
				var label = Util.FindComponent<UILabel>(go);
				if (label != null)
				{
					Util.FindComponent<BoxCollider>(go, true);
				}
			
				var sprite = Util.FindComponent<UISprite>(go);
				if (sprite != null)
				{
					var box = Util.FindComponent<BoxCollider>(go, true);
					box.center = new Vector3(
						Mathf.RoundToInt(box.center.x), 
						Mathf.RoundToInt(box.center.y), 0.1f); // in case
					box.size = new Vector3(
						Mathf.RoundToInt(w), 
						Mathf.RoundToInt(h), 1);
				}
			}
			
			if (c.type == PsdLayerCommandParser.ControlType.Texture)
			{
				var tex = Util.FindComponent<UITexture>(go);
				if (tex != null)
				{
					tex.width = Mathf.RoundToInt(w);
					tex.height = Mathf.RoundToInt(h);
				}
			}
		}
		if (thisWidget as UILabel != null)
		{
			thisWidget.MakePixelPerfect();
		}
		
		// set position
		
		var x = (float)c.area.left;
		var y = (float)c.area.top;
		{
			x -= (float)PsdLayerToNGUI.data.targetWidth * 0.5f;
			y -= (float)PsdLayerToNGUI.data.targetHeight * 0.5f;
			x += (float)w * 0.5f;
			y += (float)h * 0.5f;
		}
		
		var parent = go.transform.parent;
		var root = Util.ClosestComponent<UIRoot>(go);
		
		var pos = new Vector3(Mathf.Round(x), Mathf.Round(-y), 0);
		pos.z = 0;
		
		go.transform.parent = root.transform;
		go.transform.localPosition = pos;
		go.transform.parent = parent;
		
		// set sprite align

		if (c.type == PsdLayerCommandParser.ControlType.Sprite)
		{
			var spr2 = Util.FindComponent<UISprite>(go);
			if (spr2 != null)
			{
				spr2.pivot = (UIWidget.Pivot)c.align;
			}
		}
		
		// set label align
		
		var labels = Util.FindComponents<UILabel>(go);
		foreach (var label in labels)
		{
			foreach (var src in c.sources)
			{
				if (src.type == PsdLayerCommandParser.ControlType.Label || 
				    src.type == PsdLayerCommandParser.ControlType.LabelButton)
				{
					label.pivot = (UIWidget.Pivot)src.align;
					break;
				}
			}
		}
	}
	
	private static void ExtractTextures(List<Texture> textures, PsdLayerExtractor extractor)
	{
		extractor.Reload();
		extractor.SaveLayersToPNGs();
		AssetDatabase.Refresh();
		
		foreach (var imageFilePath in extractor.ImageFilePathes)
		{
			var tex = AssetDatabase.LoadMainAssetAtPath(imageFilePath.filePath) as Texture2D;
			if (tex == null)
			{
				Debug.LogError("Cannot found texture assets. Please check " + imageFilePath.filePath + " file.");
				return;
			}
			
			var exist = false;
			foreach (var t in textures)
			{
				if (t.name == tex.name)
				{
					exist = true;
					break;
				}
			}
			if (!exist)
				textures.Add(tex);
		}
	}
	
	private static List<Texture> ExtractTextures(PsdLayerExtractor extractor)
	{
		var textures = new List<Texture>();
		PsdLayerToNGUI.ExtractTextures(textures, extractor);
		return textures;
	}
	
	#region Creation Methods
	
	private static void CreateNGUIAtlas(PsdLayerExtractor extractor, List<Texture> textures)
	{
		var currentPath = PsdLayerToNGUI.CurrentPath;
		var name = extractor.PsdFileName.Substring(0, extractor.PsdFileName.Length - 4);
		var makeNewAtlas = false;
		{
			if (PsdLayerToNGUI.data.useOneAtlas)
				makeNewAtlas = PsdLayerToNGUI.data.atlasPrefab == null;
			else
				makeNewAtlas = true;
		}
		
		// Font
		
		var isAddFont = false;
		if (PsdLayerToNGUI.data.addFont && PsdLayerToNGUI.data.fontType == Data.FontType.Bitmap)
		{
			if (PsdLayerToNGUI.data.useOneAtlas)
			{
				isAddFont = PsdLayerToNGUI.data.extractors[0] == extractor; // make only once
			}
			else
			{
				isAddFont = extractor.IsAddFont;
			}
		}
		if (isAddFont)
		{
			NGUIFontCreator.PrepareBitmap(currentPath + name, PsdLayerToNGUI.data);
			
			if (PsdLayerToNGUI.data.fontType == Data.FontType.Bitmap){
				textures.Add(PsdLayerToNGUI.data.fontTexture);
			}
		}
		
		// Atlas
		
		if (makeNewAtlas)
		{
			var collectionName = name + "_Atlas";
			
			PsdLayerToNGUI.data.atlasPrefab = null;
			NGUIAtlasMaker.CreateAtlas(currentPath + collectionName);
			PsdLayerToNGUI.data.atlasPrefab = NGUISettings.atlas;
		}
		else
			NGUISettings.atlas = PsdLayerToNGUI.data.atlasPrefab;
		
		NGUIAtlasMaker.UpdateAtlas(textures, PsdLayerToNGUI.data.keepOldSprites);
		AssetDatabase.Refresh();
		
		// Font2
		
		if (isAddFont)
		{
			NGUIFontCreator.CreateBitmap(PsdLayerToNGUI.data);
		}
		Resources.UnloadUnusedAssets();
	}
	
	private static void CreateNGUIs(PsdLayerExtractor extractor)
	{
		if (!PsdLayerToNGUI.data.createControls || PsdLayerToNGUI.data.atlasPrefab == null){
			return;
		}
		
		PsdLayerToNGUI.data.nguiDepth = 0;
		PsdLayerToNGUI.data.nguiPanelDepth = 0;

		NGUISettings.atlas = PsdLayerToNGUI.data.atlasPrefab;
		
		if (PsdLayerToNGUI.data.nguiRoot == null)
		{
			PsdLayerToNGUI.data.nguiRoot = GameObject.FindObjectOfType(typeof(UIRoot)) as UIRoot;
			if (PsdLayerToNGUI.data.nguiRoot != null){
				var go = PsdLayerToNGUI.data.nguiRoot.gameObject;
				PsdLayerToNGUI.data.nguiRootId = Util.FindComponent<PsdLayerGuid>(go, true).guid;
			}
			else{
				var go = NGUIRootCreator.CreateNewUI().transform.root.gameObject;
				PsdLayerToNGUI.data.nguiRoot = go.GetComponent<UIRoot>();
				PsdLayerToNGUI.data.nguiRootId = go.AddComponent<PsdLayerGuid>().guid;
			}
		}

		var owner = extractor.Link();
		{
			var anchor = Util.FindComponent<UIAnchor>(PsdLayerToNGUI.data.nguiRoot);
			if (anchor != null)
				owner.transform.parent = anchor.transform;
			else
				owner.transform.parent = PsdLayerToNGUI.data.nguiRoot.transform;
			
			owner.transform.localScale = Vector3.one;
			var panel = owner.AddComponent<UIPanel>();
			panel.depth = PsdLayerToNGUI.data.nguiPanelDepth++;
		}
		PsdLayerToNGUI.CreateNGUIs(PsdLayerToNGUI.data.nguiRootGameObject, owner, extractor.PsdFileName, extractor.Root.children);
		owner.transform.localPosition = Vector3.zero;

		var uicam = Util.FindComponent<UICamera>(PsdLayerToNGUI.data.nguiRoot);
		var cam = Util.FindComponent<Camera>(uicam);
		Debug.Log ("Camera Mask = " + LayerMask.LayerToName(cam.cullingMask) + "(" + cam.cullingMask + ")");

		if (LayerMask.LayerToName(cam.cullingMask) != "Everything")
			cam.cullingMask = 1;
	}
	
	private static void CreateNGUIs(GameObject rootGo, GameObject owner,
		string psdFileName, List<PsdLayerExtractor.Layer> layers)
	{
		if (!PsdLayerToNGUI.data.createControls)
			return;
		
		var root = Util.FindComponent<UIRoot>(rootGo);
		{
			root.manualHeight = 
			root.maximumHeight = 
			root.minimumHeight = PsdLayerToNGUI.data.targetHeight;
		}
		var anchor = Util.FindComponent<UIAnchor>(root);
		if (anchor != null)
		{
			anchor.side = UIAnchor.Side.Center;
		}
		
		var pa = new PsdLayerCommandParser();
		pa.Parse(psdFileName, layers);

		foreach (var c in pa.root.children)
		{
			PsdLayerToNGUI.CreateNGUI(owner, c);
		}
	}
	
	private static bool CreateNGUI(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var succeeded = false;
		var moveAndResize = true;
		
		// try to create dynamic font
		
		var extractor = PsdLayerToNGUI.data.currentExtractor;
		if (PsdLayerToNGUI.data.fontType == Data.FontType.Dynamic && extractor != null)
		{
			var dynamicFontContainer = PsdLayerToNGUI.FindDynamicFontContainer(c);
			if (dynamicFontContainer != null && dynamicFontContainer.fontSize > 0)
			{
				var currentPath = PsdLayerToNGUI.CurrentPath;
				var name = extractor.PsdFileName.Substring(0, extractor.PsdFileName.Length - 4);
				NGUIFontCreator.CreateDynamic(currentPath + name, PsdLayerToNGUI.data, dynamicFontContainer);
			}
		}
		
		switch (c.type)
		{
		case PsdLayerCommandParser.ControlType.Container:
		case PsdLayerCommandParser.ControlType.Panel:
		case PsdLayerCommandParser.ControlType.SpriteFont:
			{
				var gg = Util.CreateEmptyGameObject(owner);
				
				if (c.type == PsdLayerCommandParser.ControlType.SpriteFont)
				{
					gg.AddComponent<PsdLayerSpriteFont>();
				}
				else if (c.type == PsdLayerCommandParser.ControlType.Panel)
				{
					var panel = gg.AddComponent<UIPanel>();
					panel.depth = PsdLayerToNGUI.data.nguiPanelDepth++;
				}
				
				foreach (var c2 in c.children)
				{
					CreateNGUI(gg, c2);
				}
				
				Util.SetXYCenterAmongChildren(gg);
				Selection.activeGameObject = gg;
			}
			succeeded = true;
			moveAndResize = false;
			break;

		case PsdLayerCommandParser.ControlType.Button:
		case PsdLayerCommandParser.ControlType.LabelButton:
			succeeded = CreateNGUIButton(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.Toggle:
			succeeded = CreateNGUIToggle(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.ComboBox:
			succeeded = CreateNGUIComboBox(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.Input:
			succeeded = CreateNGUIEditBox(owner, c, false);
			break;
			
		case PsdLayerCommandParser.ControlType.Password:
			succeeded = CreateNGUIEditBox(owner, c, true);
			break;
			
		case PsdLayerCommandParser.ControlType.Label:
			succeeded = CreateNGUILabel(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.VScrollBar:
			succeeded = CreateNGUIScrollBar(owner, c, true);
			break;
			
		case PsdLayerCommandParser.ControlType.HScrollBar:
			succeeded = CreateNGUIScrollBar(owner, c, false);
			break;
			
		case PsdLayerCommandParser.ControlType.ScrollView:
			succeeded = CreateNGUIScrollView(owner, c);
			moveAndResize = false;
			break;
			
		case PsdLayerCommandParser.ControlType.VirtualView:
			succeeded = CreateVirtualView(owner, c);
			moveAndResize = false;
			break;
			
		case PsdLayerCommandParser.ControlType.Texture:
			succeeded = CreateTexture(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.SpriteAnimation:
			succeeded = CreateNGUIAnimation(owner, c);
			moveAndResize = false;
			break;

		case PsdLayerCommandParser.ControlType.Script:
			break;
			
		default:
			succeeded = CreateNGUISprite(owner, c);
			break;
		}
		
		if (succeeded)
		{
			var go = Selection.activeGameObject;
			go.name = c.name;
			
			for (var i=0; i<c.sources.Count; ++i)
			{
				if (c.sources[i].type == PsdLayerCommandParser.ControlType.Script)
				{
					var userScript = go.AddComponent<PsdLayerUserScript>();
					userScript.data = c.sources[i].text;
				}
			}
			
			if (moveAndResize)
			{
				PsdLayerToNGUI.MoveAndResize(go, c);
			}
		}
		return succeeded;
	}
	
	private static bool CreateNGUISprite(GameObject owner, PsdLayerCommandParser.Control c)
	{
		PsdLayerToNGUI.SetSlicedSprite(c.fullName, c.sliceArea);
		NGUIWidgetCreator.CreateSprite(owner, c.fullName, c.sliceArea != null );
		return true;
	}

	private static bool CreateNGUIButton(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"button", new string[]{".normal", ".hover", ".pressed", ".label"});

		var normal = arr[0];
		var hover = arr[1];
		var push = arr[2];
		var label = arr[3];
		
		if (normal != null && push  != null)
		{
			NGUIWidgetCreator.mImage0 = normal.fullName;
			NGUIWidgetCreator.mImage1 = hover == null ? normal.fullName : hover.fullName;
			NGUIWidgetCreator.mImage2 = push.fullName;
			NGUIWidgetCreator.CreateImageButton(owner);
		}
		else if (normal != null && c.type == PsdLayerCommandParser.ControlType.Button)
		{
			NGUIWidgetCreator.mButton = normal.fullName;
			NGUIWidgetCreator.CreateButton(owner);
		}
		else if (c.type == PsdLayerCommandParser.ControlType.LabelButton)
		{
			NGUIWidgetCreator.mButton = normal.fullName;
			NGUIWidgetCreator.CreateButton(owner);
			label = normal;
		}
		else
		{
			return false;
		}
		
		var uilabel = Util.FindComponent<UILabel>(Selection.activeGameObject);
		PsdLayerToNGUI.SetButtonAppearance(Selection.activeGameObject, normal, uilabel, true);
		PsdLayerToNGUI.SetLabelAppearance(uilabel, label);
		
		return true;
	}
	
	private static bool CreateNGUIComboBox(GameObject owner, PsdLayerCommandParser.Control c)
	{
		if (NGUISettings.ambigiousFont == null)
			return false;
		
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"combobox", new string[]{".bg", ".bar", ".listbg", ".label"});

		var bg = arr[0];
		var bar = arr[1];
		var listbg = arr[2];
		var label = arr[3];
		
		if (bg != null)
		{
			NGUIWidgetCreator.mListBG = bg.fullName;
			NGUIWidgetCreator.mListFG = bar == null ? bg.fullName : bar.fullName;
			NGUIWidgetCreator.mListHL = listbg == null ? bg.fullName : listbg.fullName;
			NGUIWidgetCreator.CreatePopup(owner, true);
		}
		else
		{
			return false;
		}
		
		PsdLayerToNGUI.SetButtonAppearance(Selection.activeGameObject, bg, null, true);
		
		var uilabel = Util.FindComponent<UILabel>(Selection.activeGameObject);
		PsdLayerToNGUI.SetLabelAppearance(uilabel, label);
		{
			var spr = Util.FindComponent<UISprite>(Selection.activeGameObject);
			uilabel.width = (int)spr.transform.localScale.x;
			uilabel.transform.localPosition = Vector3.zero;
		}
		
		var p = Util.FindComponent<UIPopupList>(Selection.activeGameObject);
		p.highlightColor = Color.gray;
		if (label != null)
		{
			p.textColor = label.color;
			if (!string.IsNullOrEmpty(label.text))
			{
				p.items.Clear();
				var lines = label.text.Split('\n');
				foreach (var line in lines)
				{
					if (!string.IsNullOrEmpty(line))
						p.items.Add(line);
				}
			}
		}
		return true;
	}
	
	private static bool CreateNGUIToggle(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"toggle,checkbox", new string[]{".normal", ".checked", ".label"});

		var normal = arr[0];
		var check = arr[1];
		var label = arr[2];
		
		if (normal != null)
		{
			NGUIWidgetCreator.mCheckBG = normal.fullName;
			NGUIWidgetCreator.mCheck = check != null ? check.fullName : normal.fullName;
			NGUIWidgetCreator.CreateToggle(owner);
		}
		else
		{
			return false;
		}
		
		PsdLayerToNGUI.SetButtonAppearance(Selection.activeGameObject, normal, null, true);
		
		var uilabel = Util.FindComponent<UILabel>(Selection.activeGameObject);
		PsdLayerToNGUI.SetLabelAppearance(uilabel, label);
		
		return true;
	}
	
	private static bool CreateNGUIEditBox(GameObject owner, PsdLayerCommandParser.Control c, bool password)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			password ? "password" : "editbox", new string[]{".bg", ".label"});

		var normal = arr[0];
		var label = arr[1];
		
		if (normal != null)
		{
			NGUIWidgetCreator.mInputBG = normal.fullName;
			NGUIWidgetCreator.CreateInput(owner, password);
			Util.FindComponent<UIInput>(Selection.activeGameObject).activeTextColor = normal.color;
		}
		else
		{
			return false;
		}
		
		var uilabel = Util.FindComponent<UILabel>(Selection.activeGameObject);
		var sprite = Util.FindComponent<UISprite>(Selection.activeGameObject);
		PsdLayerToNGUI.SetLabelAppearance(uilabel, label, sprite.localSize);
		
		return true;
	}
	
	private static bool CreateNGUILabel(GameObject owner, PsdLayerCommandParser.Control c)
	{
		NGUIWidgetCreator.CreateLabel(owner);
		
		var label = Util.FindComponent<UILabel>(Selection.activeGameObject);
		PsdLayerToNGUI.SetLabelAppearance(label, c);
		
		return true;
	}
	
	private static bool CreateNGUIScrollBar(GameObject owner, PsdLayerCommandParser.Control c, bool vertical)
	{
		//**todo
//		var arr = PsdLayerToNGUI.FindControlSources(c, 
//			"scrollbar", new string[]{".bg", ".fg"});
//		
//		var scrollbar = null as UIScrollBar;
//		var scrollbar_bg = arr[0];
//		var scrollbar_fg = arr[1];
//		
//		if (scrollbar_bg != null || scrollbar_fg != null)
//		{
//			NGUIWidgetCreator.mScrollBG = 
//				scrollbar_bg != null ? scrollbar_bg.fullName : scrollbar_fg.fullName;
//			
//			NGUIWidgetCreator.mScrollFG = 
//				scrollbar_fg != null ? scrollbar_fg.fullName : scrollbar_bg.fullName;
//			
//			NGUIWidgetCreator.CreateScrollBar(owner);
//			
//			scrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//			scrollbar.direction = UIScrollBar.Direction.Vertical;
//			return false;
//		}
//		else
//			return false;
//		
//		if (scrollbar != null)
//		{
//			PsdLayerToNGUI.MoveAndResize(scrollbar.gameObject, scrollbar_bg);
//			
//			var go = scrollbar.gameObject;
//			var c2 = scrollbar_bg;
//			var x = (float)c2.area.left;
//			var y = (float)c2.area.top;
//			if (vertical)
//			{
//				x -= (float)targetWidth/2f;
//				x += (float)c2.area.width/2f;
//				y -= (float)targetHeight/2f;
//			}
//			else
//			{
//				x -= (float)targetWidth/2f;
//				y -= (float)targetHeight/2f;
//				y += (float)c2.area.height/2f;
//			}
//			var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//			pos.z = go.transform.localPosition.z;
//			go.transform.localPosition = pos;
//		}
		return false;
	}
	
	private static bool CreateNGUIScrollView(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"scrollview", new string[]{
			"scrollview.bg", ".item", 
			".vscrollbar.bg", ".vscrollbar.fg", 
			".hscrollbar.bg", ".hscrollbar.fg"
		});
		
		var bg = arr[0];
		var item = arr[1];
		//**todo
//		var vscrollbar_bg = arr[2];
//		var vscrollbar_fg = arr[3];
//		var hscrollbar_bg = arr[4];
//		var hscrollbar_fg = arr[5];
		
		if (bg != null)
		{
			owner = Util.CreateEmptyGameObject(owner);
			owner.name = bg.name;
			
			var bgSprite = null as GameObject;
			{
				NGUIWidgetCreator.CreateSprite(owner, c.fullName, bg.sliceArea != null );
				
				bgSprite = Selection.activeGameObject;
				bgSprite.AddComponent<BoxCollider>();
				bgSprite.name = "Bg";
				
				PsdLayerToNGUI.MoveAndResize(bgSprite, bg);
			}
			
			//**todo
//			var vscrollbar = null as UIScrollBar;
//			if (vscrollbar_bg != null || vscrollbar_fg != null)
//			{
//				NGUIWidgetCreator.mScrollBG = 
//					vscrollbar_bg != null ? vscrollbar_bg.fullName : vscrollbar_fg.fullName;
//				
//				NGUIWidgetCreator.mScrollFG = 
//					vscrollbar_fg != null ? vscrollbar_fg.fullName : vscrollbar_bg.fullName;
//				
//				NGUIWidgetCreator.CreateScrollBar(owner);
//				
//				vscrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//				vscrollbar.direction = UIScrollBar.Direction.Vertical;
//			}
//			if (vscrollbar != null)
//			{
//				PsdLayerToNGUI.MoveAndResize(vscrollbar.gameObject, vscrollbar_bg);
//				
//				var go = vscrollbar.gameObject;
//				var c2 = vscrollbar_bg;
//				var x = (float)c2.area.left;
//				var y = (float)c2.area.top;
//				{
//					x -= (float)targetWidth/2f;
//					x += (float)c2.area.width/2f;
//					y -= (float)targetHeight/2f;
//					
//					var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//					pos.z = go.transform.localPosition.z;
//					go.transform.localPosition = pos;
//				}
//			}
//			
//			var hscrollbar = null as UIScrollBar;
//			if (hscrollbar_bg != null || hscrollbar_fg != null)
//			{
//				NGUIWidgetCreator.mScrollBG = 
//					hscrollbar_bg != null ? hscrollbar_bg.fullName : hscrollbar_fg.fullName;
//				
//				NGUIWidgetCreator.mScrollFG = 
//					hscrollbar_fg != null ? hscrollbar_fg.fullName : hscrollbar_bg.fullName;
//				
//				NGUIWidgetCreator.CreateScrollBar(owner);
//				
//				hscrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//				hscrollbar.direction = UIScrollBar.Direction.Horizontal;
//			}
//			if (hscrollbar != null)
//			{
//				PsdLayerToNGUI.MoveAndResize(hscrollbar.gameObject, hscrollbar_bg);
//				
//				var go = hscrollbar.gameObject;
//				var c2 = hscrollbar_bg;
//				var x = (float)c2.area.left;
//				var y = (float)c2.area.top;
//				{
//					x -= (float)targetWidth/2f;
//					y -= (float)targetHeight/2f;
//					y += (float)c2.area.height/2f;
//					
//					var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//					pos.z = go.transform.localPosition.z;
//					go.transform.localPosition = pos;
//				}
//			}
			
			var panelgo = Util.CreateEmptyGameObject(owner);
			panelgo.name = "ScrollView";
			panelgo.transform.localPosition = new Vector3(0, 0 -1);
			{
				var panel = panelgo.AddComponent<UIPanel>();
				panel.depth = PsdLayerToNGUI.data.nguiPanelDepth++;

				var view = panelgo.AddComponent<UIScrollView>();
				//**todo
//				view.verticalScrollBar = vscrollbar;
//				view.horizontalScrollBar = hscrollbar;
				view.showScrollBars = UIScrollView.ShowCondition.WhenDragging;
				view.movement = UIScrollView.Movement.Vertical;
				
				Util.FindComponent<UIDragScrollView>(bgSprite, true).scrollView = view;
				
				if (item != null)
				{
					foreach (var child in item.children)
						CreateNGUI(panelgo, child);
					
					Util.SetXYCenterAmongChildren(panelgo);
				}
				
				panel.clipping = UIDrawCall.Clipping.SoftClip;
				panel.baseClipRegion = new Vector4(
					panel.transform.localPosition.x, 
					-panel.transform.localPosition.y, bg.area.width, bg.area.height);
			}

			Selection.activeGameObject = owner;
		}
		else
		{
			return false;
		}
		
		return true;
	}
	
	private static bool CreateVirtualView(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"virtualview", new string[]{
			"virtualview.bg", ".item", 
			".vscrollbar.bg", ".vscrollbar.fg", 
			".hscrollbar.bg", ".hscrollbar.fg"
		});
		
		var bg = arr[0];
		var item = arr[1];
		//**todo
//		var vscrollbar_bg = arr[2];
//		var vscrollbar_fg = arr[3];
//		var hscrollbar_bg = arr[4];
//		var hscrollbar_fg = arr[5];
		
		if (bg != null)
		{
			owner = Util.CreateEmptyGameObject(owner);
			owner.name = bg.name;
			
			var bgSprite = null as GameObject;
			{
				NGUIWidgetCreator.CreateSprite(owner, c.fullName, bg.sliceArea != null);
				
				bgSprite = Selection.activeGameObject;
				bgSprite.name = "Bg";
				
				PsdLayerToNGUI.MoveAndResize(bgSprite, bg);
			}
			
		//**todo
//			var vscrollbar = null as UIScrollBar;
//			if (vscrollbar_bg != null || vscrollbar_fg != null)
//			{
//				NGUIWidgetCreator.mScrollBG = 
//					vscrollbar_bg != null ? vscrollbar_bg.fullName : vscrollbar_fg.fullName;
//				
//				NGUIWidgetCreator.mScrollFG = 
//					vscrollbar_fg != null ? vscrollbar_fg.fullName : vscrollbar_bg.fullName;
//				
//				NGUIWidgetCreator.CreateScrollBar(owner);
//				
//				vscrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//				vscrollbar.direction = UIScrollBar.Direction.Vertical;
//			}
//			if (vscrollbar != null)
//			{
//				PsdLayerToNGUI.MoveAndResize(vscrollbar.gameObject, vscrollbar_bg);
//				
//				var go = vscrollbar.gameObject;
//				var c2 = vscrollbar_bg;
//				var x = (float)c2.area.left;
//				var y = (float)c2.area.top;
//				{
//					x -= (float)targetWidth/2f;
//					x += (float)c2.area.width/2f;
//					y -= (float)targetHeight/2f;
//					
//					var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//					pos.z = go.transform.localPosition.z;
//					go.transform.localPosition = pos;
//				}
//			}
//			
//			var hscrollbar = null as UIScrollBar;
//			if (hscrollbar_bg != null || hscrollbar_fg != null)
//			{
//				NGUIWidgetCreator.mScrollBG = 
//					hscrollbar_bg != null ? hscrollbar_bg.fullName : hscrollbar_fg.fullName;
//				
//				NGUIWidgetCreator.mScrollFG = 
//					hscrollbar_fg != null ? hscrollbar_fg.fullName : hscrollbar_bg.fullName;
//				
//				NGUIWidgetCreator.CreateScrollBar(owner);
//				
//				hscrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//				hscrollbar.direction = UIScrollBar.Direction.Horizontal;
//			}
//			if (hscrollbar != null)
//			{
//				PsdLayerToNGUI.MoveAndResize(hscrollbar.gameObject, hscrollbar_bg);
//				
//				var go = hscrollbar.gameObject;
//				var c2 = hscrollbar_bg;
//				var x = (float)c2.area.left;
//				var y = (float)c2.area.top;
//				{
//					x -= (float)targetWidth/2f;
//					y -= (float)targetHeight/2f;
//					y += (float)c2.area.height/2f;
//					
//					var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//					pos.z = go.transform.localPosition.z;
//					go.transform.localPosition = pos;
//				}
//			}
			
			var view = owner.AddComponent<PsdLayerVirtualView>();
			view.bg = bgSprite.transform;
			{
				//**todo
//				view.verticalScrollBar = vscrollbar;
//				view.horizontalScrollBar = hscrollbar;

				var box = owner.AddComponent<BoxCollider>();
				box.center = bgSprite.transform.localPosition;
				box.size = new Vector3(bg.area.width, bg.area.height, 0);

				var itemsGo = Util.FindGameObject(view, "Items", true);
				var itemGo = Util.FindGameObject(itemsGo, "Item", true);
				{
					var panel = itemsGo.AddComponent<UIPanel>();
					panel.depth = PsdLayerToNGUI.data.nguiPanelDepth++;
					panel.clipping = UIDrawCall.Clipping.SoftClip;
					panel.baseClipRegion = new Vector4(
						bgSprite.transform.localPosition.x, 
						bgSprite.transform.localPosition.y, bg.area.width, bg.area.height);

					foreach (var child in item.children)
					{
						CreateNGUI(itemGo, child);
					}
					view.item = itemGo.transform;
				}
				Util.SetXYCenterAmongChildren(itemGo);
			}
			Selection.activeGameObject = view.gameObject;
		}
		else
		{
			return false;
		}
		
		return true;
	}
	
	private static bool CreateTexture(GameObject owner, PsdLayerCommandParser.Control c)
	{
		NGUIWidgetCreator.CreateSimpleTexture(owner);
		
		var tex = Util.FindComponent<UITexture>(Selection.activeGameObject);
		if (tex != null)
		{
			//**todo
			//tex.mainTexture = AssetDatabase.LoadAssetAtPath(c.pa.srcFilePath, typeof(Texture2D)) as Texture2D;
			return true;
		}
		return false;
	}
	
	private static bool CreateNGUIAnimation(GameObject owner, PsdLayerCommandParser.Control c)
	{
		if (PsdLayerToNGUI.CreateNGUISprite(owner, c) && c.children.Count > 0){
			var ani = Selection.activeGameObject.AddComponent<PsdLayerSpriteAnimation>();
			var names = new List<string>();
			for (var i=0; i<c.children.Count; ++i){
				names.Add(c.children[i].fullName);
			}
			names.Sort();
			ani.SetSprites(names.ToArray());
			ani.fps = c.fps;
			
			PsdLayerToNGUI.MoveAndResize(ani.gameObject, c.children[0]);
			return true;
		}
		return false;
	}

	#endregion
	
	#region Update Methods
	
	private static List<PsdLayerExtractor> updatingExtractor = new List<PsdLayerExtractor>();
	
	private static void AddPsdFileToUpdate(PsdLayerExtractor extractor)
	{
		lock (PsdLayerToNGUI.updatingExtractor)
		{
			if (!PsdLayerToNGUI.updatingExtractor.Contains(extractor))
				PsdLayerToNGUI.updatingExtractor.Add(extractor);
		}
	}
	private static void UpdatePsdFile()
	{
		lock (PsdLayerToNGUI.updatingExtractor)
		{
			if (PsdLayerToNGUI.updatingExtractor.Count > 0)
				PsdLayerToNGUI.Save();
		}
		
		PsdLayerExtractor extractor;
		while (true)
		{
			lock (PsdLayerToNGUI.updatingExtractor)
			{
				if (PsdLayerToNGUI.updatingExtractor.Count == 0)
					break;
				extractor = PsdLayerToNGUI.updatingExtractor[0];
				PsdLayerToNGUI.updatingExtractor.RemoveAt(0);
			}
			
			if (extractor.IsLinked)
			{
				var textures = PsdLayerToNGUI.ExtractTextures(extractor);
				PsdLayerToNGUI.UpdateNGUIAtlas(extractor, textures);
				PsdLayerToNGUI.UpdateNGUIs(extractor);
				Debug.Log(extractor.PsdFilePath + " updated");
			}
			else
				Debug.Log(extractor.PsdFilePath + " has no link with hierarchy");
		}
	}
	
	private static void UpdateNGUIAtlas(PsdLayerExtractor extractor, List<Texture> textures)
	{
		var currentPath = PsdLayerToNGUI.CurrentPath;
		var name = extractor.PsdFileName.Substring(0, extractor.PsdFileName.Length - 4);
		
		var spr = Util.FindComponent<UISprite>(extractor.RootGameObject);
		if (spr == null || spr.atlas == null)
		{
			Debug.LogError("Cannot find atals for " + extractor.PsdFilePath);
			return;
		}
		PsdLayerToNGUI.data.atlasPrefab = spr.atlas;
		NGUISettings.atlas = spr.atlas;
		
		var label = Util.FindComponent<UILabel>(extractor.RootGameObject);
		if (label != null){
			NGUISettings.ambigiousFont = label.ambigiousFont;
		}
		
		// Font
		
		var isAddFont = false;
		if (PsdLayerToNGUI.data.addFont && PsdLayerToNGUI.data.fontType == Data.FontType.Bitmap)
		{
			if (PsdLayerToNGUI.data.useOneAtlas)
			{
				isAddFont = PsdLayerToNGUI.data.extractors[0] == extractor; // make only once
			}
			else
			{
				isAddFont = extractor.IsAddFont;
			}
		}
		if (isAddFont)
		{
			NGUIFontCreator.PrepareBitmap(currentPath + name, PsdLayerToNGUI.data);
			
			if (PsdLayerToNGUI.data.fontType == Data.FontType.Bitmap){
				textures.Add(PsdLayerToNGUI.data.fontTexture);
			}
		}
		
		// Atlas
		
		NGUISettings.atlas = PsdLayerToNGUI.data.atlasPrefab;
		NGUIAtlasMaker.UpdateAtlas(textures, PsdLayerToNGUI.data.keepOldSprites);
		AssetDatabase.Refresh();
		
		// Font2
		
		if (isAddFont)
		{
			NGUIFontCreator.CreateBitmap(PsdLayerToNGUI.data);
		}
		Resources.UnloadUnusedAssets();
	}
	
	private static void UpdateNGUIs(PsdLayerExtractor extractor)
	{
		if (PsdLayerToNGUI.data.nguiRoot == null || PsdLayerToNGUI.data.atlasPrefab == null){
			return;
		}
		
		PsdLayerToNGUI.data.nguiDepth = 0;
		PsdLayerToNGUI.data.nguiPanelDepth = 0;

		NGUISettings.atlas = PsdLayerToNGUI.data.atlasPrefab;
		
		var owner = Util.FindGameObjectRecursively(PsdLayerToNGUI.data.nguiRoot, extractor.PsdFileName);
		if (owner == null){
			owner = new GameObject(extractor.PsdFileName);
		}

		PsdLayerToNGUI.UpdateNGUIs(owner, extractor.PsdFileName, extractor.Root.children);
		owner.transform.localPosition = Vector3.zero;
	}
	
	private static void UpdateNGUIs(GameObject owner, 
		string psdFileName, List<PsdLayerExtractor.Layer> layers)
	{
		var pa = new PsdLayerCommandParser();
		pa.Parse(psdFileName, layers);

		foreach (var c in pa.root.children)
		{
			if (!PsdLayerToNGUI.UpdateNGUI(owner, c))
				PsdLayerToNGUI.CreateNGUI(owner, c);
		}
	}
	
	private static bool UpdateNGUI(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var succeeded = false;
		var moveAndResize = true;
		
		switch (c.type)
		{
		case PsdLayerCommandParser.ControlType.Container:
		case PsdLayerCommandParser.ControlType.Panel:
		case PsdLayerCommandParser.ControlType.SpriteFont:
			{
				var gg = Util.FindGameObjectRecursively(owner, c.name, true);
				if (gg == null){
					gg = Util.CreateEmptyGameObject(owner);
				}
				
				if (c.type == PsdLayerCommandParser.ControlType.SpriteFont)
				{
					Util.FindComponent<PsdLayerSpriteFont>(gg, true);
				}
				else if (c.type == PsdLayerCommandParser.ControlType.Panel)
				{
					var panel = Util.FindComponent<UIPanel>(gg, true);
					panel.depth = PsdLayerToNGUI.data.nguiPanelDepth++;
				}
				
				foreach (var c2 in c.children)
				{
					if (!UpdateNGUI(gg, c2)){
						CreateNGUI(gg, c2);
					}
				}
				
				Util.SetXYCenterAmongChildren(gg);
				Selection.activeGameObject = gg;
			}
			succeeded = true;
			moveAndResize = false;
			break;
			
		case PsdLayerCommandParser.ControlType.Button:
		case PsdLayerCommandParser.ControlType.LabelButton:
			succeeded = UpdateNGUIButton(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.Toggle:
			succeeded = UpdateNGUIToggle(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.ComboBox:
			succeeded = UpdateNGUIComboBox(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.Input:
			succeeded = UpdateNGUIEditBox(owner, c, false);
			break;
			
		case PsdLayerCommandParser.ControlType.Password:
			succeeded = UpdateNGUIEditBox(owner, c, true);
			break;
			
		case PsdLayerCommandParser.ControlType.Label:
			succeeded = UpdateNGUILabel(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.VScrollBar:
			succeeded = UpdateNGUIScrollBar(owner, c, true);
			break;
			
		case PsdLayerCommandParser.ControlType.HScrollBar:
			succeeded = UpdateNGUIScrollBar(owner, c, false);
			break;
			
		case PsdLayerCommandParser.ControlType.ScrollView:
			succeeded = UpdateNGUIScrollView(owner, c);
			moveAndResize = false;
			break;
			
		case PsdLayerCommandParser.ControlType.VirtualView:
			succeeded = UpdateVirtualView(owner, c);
			moveAndResize = false;
			break;
			
		case PsdLayerCommandParser.ControlType.Texture:
			succeeded = UpdateTexture(owner, c);
			break;
			
		case PsdLayerCommandParser.ControlType.SpriteAnimation:
			succeeded = UpdateNGUIAnimation(owner, c);
			moveAndResize = false;
			break;

		case PsdLayerCommandParser.ControlType.Script:
			break;
			
		default:
			succeeded = UpdateNGUISprite(owner, c);
			break;
		}
		
		if (succeeded)
		{
			var go = Selection.activeGameObject;
			go.name = c.name;
			
			for (var i=0; i<c.sources.Count; ++i)
			{
				if (c.sources[i].type == PsdLayerCommandParser.ControlType.Script)
				{
					var userScript = Util.FindComponent<PsdLayerUserScript>(go, true);
					userScript.data = c.sources[i].text;
				}
			}
			
			if (moveAndResize)
			{
				PsdLayerToNGUI.MoveAndResize(go, c);
			}
		}
		return succeeded;
	}
	
	private static bool UpdateNGUISprite(GameObject owner, PsdLayerCommandParser.Control c)
	{
		SetSlicedSprite(c.fullName, c.sliceArea);
		Selection.activeGameObject = Util.FindGameObjectRecursively(owner, c.name, true);
		return Selection.activeGameObject != null;
	}

	private static bool UpdateNGUIButton(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"button", new string[]{".normal", ".hover", ".pressed", ".label"});

		var normal = arr[0];
		var hover = arr[1];
		var push = arr[2];
		var label = arr[3];
		
		Selection.activeGameObject = Util.FindGameObjectRecursively(owner, c.name, true);
		if (Selection.activeGameObject == null)
			return false;
		var ib = Util.FindComponent<UIImageButton>(Selection.activeGameObject);
		var b = Util.FindComponent<UIButton>(Selection.activeGameObject);
		var uilabel = Util.FindComponent<UILabel>(Selection.activeGameObject);
		
		if (normal != null && push  != null)
		{
			if (ib != null)
			{
				ib.normalSprite = normal.fullName;
				ib.hoverSprite = hover == null ? normal.fullName : hover.fullName;
				ib.pressedSprite = push.fullName;
			}
			else if (b != null && b.tweenTarget != null)
			{
				Util.FindComponent<UISprite>(b.tweenTarget).spriteName = normal.fullName;
			}
			else
				return false;
		}
		else if (normal != null)
		{
			if (ib != null)
			{
				ib.normalSprite = normal.fullName;
				ib.hoverSprite = hover == null ? normal.fullName : hover.fullName;
				ib.pressedSprite = hover == null ? normal.fullName : hover.fullName;
			}
			else if (b != null && b.tweenTarget != null)
			{
				var spr = Util.FindComponent<UISprite>(b.tweenTarget);
				if (spr != null)
					spr.spriteName = normal.fullName;
			}
			else
				return false;
			
			if (uilabel != null && normal.type == PsdLayerCommandParser.ControlType.LabelButton)
			{
				label = normal;
			}
		}
		else
		{
			return false;
		}
		
		PsdLayerToNGUI.SetButtonAppearance(Selection.activeGameObject, normal, uilabel, true);
		PsdLayerToNGUI.SetLabelAppearance(uilabel, label);
		
		return true;
	}
	
	private static bool UpdateNGUIComboBox(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"combobox", new string[]{".bg", ".bar", ".listbg", ".label"});

		var bg = arr[0];
		var bar = arr[1];
		var listbg = arr[2];
		var label = arr[3];
		
		Selection.activeGameObject = Util.FindGameObjectRecursively(owner, c.name, true);
		if (Selection.activeGameObject == null)
			return false;
		var pl = Util.FindComponent<UIPopupList>(Selection.activeGameObject);
		var b = Util.FindComponent<UIButton>(Selection.activeGameObject);
		
		if (bg != null)
		{
			if (pl != null && b != null)
			{
				pl.backgroundSprite = bg.fullName;
				pl.highlightSprite = listbg == null ? bg.fullName : listbg.fullName;
				Util.FindComponent<UISprite>(b.tweenTarget).spriteName = bar == null ? bg.fullName : bar.fullName;
			}
			else
				return false;
		}
		else
		{
			return false;
		}
		
		PsdLayerToNGUI.SetButtonAppearance(Selection.activeGameObject, bg, null, true);
		
		var uilabel = Util.FindComponent<UILabel>(Selection.activeGameObject);
		PsdLayerToNGUI.SetLabelAppearance(uilabel, label);
		{
			var spr = Util.FindComponent<UISprite>(Selection.activeGameObject);
			uilabel.width = (int)spr.transform.localScale.x;
			uilabel.transform.localPosition = Vector3.zero;
		}
		
		var p = Util.FindComponent<UIPopupList>(Selection.activeGameObject);
		p.highlightColor = Color.gray;
		if (label != null)
		{
			p.textColor = label.color;
			if (!string.IsNullOrEmpty(label.text))
			{
				p.items.Clear();
				var lines = label.text.Split('\n');
				foreach (var line in lines)
				{
					if (!string.IsNullOrEmpty(line))
						p.items.Add(line);
				}
			}
		}
		return true;
	}
	
	private static bool UpdateNGUIToggle(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"checkbox,toggle", new string[]{".normal", ".checked", ".label"});

		var normal = arr[0];
		var check = arr[1];
		var label = arr[2];
		
		Selection.activeGameObject = Util.FindGameObjectRecursively(owner, c.name, true);
		if (Selection.activeGameObject == null)
			return false;
		var cb = Util.FindComponent<UIToggle>(Selection.activeGameObject);
		
		if (normal != null)
		{
			if (cb != null)
			{
				var btn = Util.FindComponent<UIButton>(cb);
				{
					Util.FindComponent<UISprite>(btn.tweenTarget).spriteName = normal.fullName;
				}
				(cb.activeSprite as UISprite).spriteName = check != null ? check.fullName : normal.fullName;
			}
			else
				return false;
		}
		else
		{
			return false;
		}
		
		PsdLayerToNGUI.SetButtonAppearance(Selection.activeGameObject, normal, null, true);
		
		var uilabel = Util.FindComponent<UILabel>(Selection.activeGameObject);
		PsdLayerToNGUI.SetLabelAppearance(uilabel, label);
		
		return true;
	}
	
	private static bool UpdateNGUIEditBox(GameObject owner, PsdLayerCommandParser.Control c, bool password)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			password ? "password" : "editbox,input", new string[]{".bg", ".label"});

		var normal = arr[0];
		var label = arr[1];
		
		Selection.activeGameObject = Util.FindGameObjectRecursively(owner, c.name, true);
		if (Selection.activeGameObject == null)
			return false;
		
		var eb = Util.FindComponent<UIInput>(Selection.activeGameObject);
		
		if (normal != null)
		{
			if (eb != null)
			{
				Util.FindComponent<UISprite>(eb).spriteName = c.fullName;
				eb.activeTextColor = label.color;
				eb.inputType = password ? UIInput.InputType.Password : UIInput.InputType.Standard;
			}
		}
		else
		{
			return false;
		}
		
		var uilabel = Util.FindComponent<UILabel>(Selection.activeGameObject);
		var sprite = Util.FindComponent<UISprite>(Selection.activeGameObject);
		PsdLayerToNGUI.SetLabelAppearance(uilabel, label, sprite.localSize);
		
		return true;
	}
	
	private static bool UpdateNGUILabel(GameObject owner, PsdLayerCommandParser.Control c)
	{
		Selection.activeGameObject = Util.FindGameObjectRecursively(owner, c.name, true);
		if (Selection.activeGameObject == null)
			return false;
		
		var label = Util.FindComponent<UILabel>(Selection.activeGameObject);
		if (label != null)
			PsdLayerToNGUI.SetLabelAppearance(label, c);
		else
			return false;
		
		return true;
	}
	
	private static bool UpdateNGUIScrollBar(GameObject owner, PsdLayerCommandParser.Control c, bool vertical)
	{
		//**todo
//		var arr = PsdLayerToNGUI.FindControlSources(c, 
//			"scrollbar", new string[]{".bg", ".fg"});
//		
//		var scrollbar = null as UIScrollBar;
//		var scrollbar_bg = arr[0];
//		var scrollbar_fg = arr[1];
//		
//		if (scrollbar_bg != null || scrollbar_fg != null)
//		{
//			NGUIWidgetCreator.mScrollBG = 
//				scrollbar_bg != null ? scrollbar_bg.fullName : scrollbar_fg.fullName;
//			
//			NGUIWidgetCreator.mScrollFG = 
//				scrollbar_fg != null ? scrollbar_fg.fullName : scrollbar_bg.fullName;
//			
//			NGUIWidgetCreator.CreateScrollBar(owner);
//			
//			scrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//			scrollbar.direction = UIScrollBar.Direction.Vertical;
//			return false;
//		}
//		else
//			return false;
//		
//		if (scrollbar != null)
//		{
//			PsdLayerToNGUI.MoveAndResize(scrollbar.gameObject, scrollbar_bg);
//			
//			var go = scrollbar.gameObject;
//			var c2 = scrollbar_bg;
//			var x = (float)c2.area.left;
//			var y = (float)c2.area.top;
//			if (vertical)
//			{
//				x -= (float)targetWidth/2f;
//				x += (float)c2.area.width/2f;
//				y -= (float)targetHeight/2f;
//			}
//			else
//			{
//				x -= (float)targetWidth/2f;
//				y -= (float)targetHeight/2f;
//				y += (float)c2.area.height/2f;
//			}
//			var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//			pos.z = go.transform.localPosition.z;
//			go.transform.localPosition = pos;
//		}
		return false;
	}
	
	private static bool UpdateNGUIScrollView(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"scrollview", new string[]{
			"scrollview.bg", ".item", 
			".vscrollbar.bg", ".vscrollbar.fg", 
			".hscrollbar.bg", ".hscrollbar.fg"
		});
		
		var bg = arr[0];
		var item = arr[1];
		//**todo
//		var vscrollbar_bg = arr[2];
//		var vscrollbar_fg = arr[3];
//		var hscrollbar_bg = arr[4];
//		var hscrollbar_fg = arr[5];
		
		if (bg != null)
		{
			owner = Util.FindGameObjectRecursively(owner, bg.name, true);
			if (owner == null)
				return false;
			
			var bgSprite = Util.FindGameObjectRecursively(owner, "Bg", true);
			if (bgSprite == null)
				return false;
			{
				PsdLayerToNGUI.MoveAndResize(bgSprite, bg);
			}
			
			//**todo
//			var vscrollbar = null as UIScrollBar;
//			if (vscrollbar_bg != null || vscrollbar_fg != null)
//			{
//				NGUIWidgetCreator.mScrollBG = 
//					vscrollbar_bg != null ? vscrollbar_bg.fullName : vscrollbar_fg.fullName;
//				
//				NGUIWidgetCreator.mScrollFG = 
//					vscrollbar_fg != null ? vscrollbar_fg.fullName : vscrollbar_bg.fullName;
//				
//				NGUIWidgetCreator.CreateScrollBar(owner);
//				
//				vscrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//				vscrollbar.direction = UIScrollBar.Direction.Vertical;
//			}
//			if (vscrollbar != null)
//			{
//				PsdLayerToNGUI.MoveAndResize(vscrollbar.gameObject, vscrollbar_bg);
//				
//				var go = vscrollbar.gameObject;
//				var c2 = vscrollbar_bg;
//				var x = (float)c2.area.left;
//				var y = (float)c2.area.top;
//				{
//					x -= (float)targetWidth/2f;
//					x += (float)c2.area.width/2f;
//					y -= (float)targetHeight/2f;
//					
//					var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//					pos.z = go.transform.localPosition.z;
//					go.transform.localPosition = pos;
//				}
//			}
//			
//			var hscrollbar = null as UIScrollBar;
//			if (hscrollbar_bg != null || hscrollbar_fg != null)
//			{
//				NGUIWidgetCreator.mScrollBG = 
//					hscrollbar_bg != null ? hscrollbar_bg.fullName : hscrollbar_fg.fullName;
//				
//				NGUIWidgetCreator.mScrollFG = 
//					hscrollbar_fg != null ? hscrollbar_fg.fullName : hscrollbar_bg.fullName;
//				
//				NGUIWidgetCreator.CreateScrollBar(owner);
//				
//				hscrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//				hscrollbar.direction = UIScrollBar.Direction.Horizontal;
//			}
//			if (hscrollbar != null)
//			{
//				PsdLayerToNGUI.MoveAndResize(hscrollbar.gameObject, hscrollbar_bg);
//				
//				var go = hscrollbar.gameObject;
//				var c2 = hscrollbar_bg;
//				var x = (float)c2.area.left;
//				var y = (float)c2.area.top;
//				{
//					x -= (float)targetWidth/2f;
//					y -= (float)targetHeight/2f;
//					y += (float)c2.area.height/2f;
//					
//					var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//					pos.z = go.transform.localPosition.z;
//					go.transform.localPosition = pos;
//				}
//			}
			
			var panelgo = Util.FindGameObjectRecursively(owner, "DraggablePanel", true);
			if (panelgo == null)
				return false;
			
			var panel = Util.FindComponent<UIPanel>(panelgo);
			var view = Util.FindComponent<UIScrollView>(panelgo);
			if (panel == null || view == null)
				return false;
			{
				panel.depth = PsdLayerToNGUI.data.nguiPanelDepth++;
				panel.clipping = UIDrawCall.Clipping.SoftClip;
				panel.baseClipRegion = new Vector4(
					bgSprite.transform.localPosition.x, 
					bgSprite.transform.localPosition.y, bg.area.width, bg.area.height);
				
				//**todo
//				view.verticalScrollBar = vscrollbar;
//				view.horizontalScrollBar = hscrollbar;
				view.showScrollBars = UIScrollView.ShowCondition.WhenDragging;
				view.movement = UIScrollView.Movement.Vertical;
//				view.repositionClipping = true;
				
				if (item != null)
				{
					foreach (var child in item.children)
					{
						if (!UpdateNGUI(panelgo, child))
							CreateNGUI(panelgo, child);
					}
					Util.SetXYCenterAmongChildren(panelgo);
				}
			}

			Selection.activeGameObject = owner;
		}
		else
		{
			return false;
		}
		
		return true;
	}
	
	private static bool UpdateVirtualView(GameObject owner, PsdLayerCommandParser.Control c)
	{
		var arr = PsdLayerToNGUI.FindControlSources(c, 
			"virtualview", new string[]{
			"virtualview.bg", ".item", 
			".vscrollbar.bg", ".vscrollbar.fg", 
			".hscrollbar.bg", ".hscrollbar.fg"
		});
		
		var bg = arr[0];
		var item = arr[1];
		//**todo
//		var vscrollbar_bg = arr[2];
//		var vscrollbar_fg = arr[3];
//		var hscrollbar_bg = arr[4];
//		var hscrollbar_fg = arr[5];
		
		if (bg != null)
		{
			owner = Util.FindGameObjectRecursively(owner, bg.name, true);
			if (owner == null)
				return false;
			
			var bgSprite = Util.FindGameObjectRecursively(owner, "Bg", true);
			if (bgSprite == null)
				return false;
			{
				PsdLayerToNGUI.MoveAndResize(bgSprite, bg);
			}
			
			//**todo
//			var vscrollbar = null as UIScrollBar;
//			if (vscrollbar_bg != null || vscrollbar_fg != null)
//			{
//				NGUIWidgetCreator.mScrollBG = 
//					vscrollbar_bg != null ? vscrollbar_bg.fullName : vscrollbar_fg.fullName;
//				
//				NGUIWidgetCreator.mScrollFG = 
//					vscrollbar_fg != null ? vscrollbar_fg.fullName : vscrollbar_bg.fullName;
//				
//				NGUIWidgetCreator.CreateScrollBar(owner);
//				
//				vscrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//				vscrollbar.direction = UIScrollBar.Direction.Vertical;
//			}
//			if (vscrollbar != null)
//			{
//				PsdLayerToNGUI.MoveAndResize(vscrollbar.gameObject, vscrollbar_bg);
//				
//				var go = vscrollbar.gameObject;
//				var c2 = vscrollbar_bg;
//				var x = (float)c2.area.left;
//				var y = (float)c2.area.top;
//				{
//					x -= (float)targetWidth/2f;
//					x += (float)c2.area.width/2f;
//					y -= (float)targetHeight/2f;
//					
//					var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//					pos.z = go.transform.localPosition.z;
//					go.transform.localPosition = pos;
//				}
//			}
//			
//			var hscrollbar = null as UIScrollBar;
//			if (hscrollbar_bg != null || hscrollbar_fg != null)
//			{
//				NGUIWidgetCreator.mScrollBG = 
//					hscrollbar_bg != null ? hscrollbar_bg.fullName : hscrollbar_fg.fullName;
//				
//				NGUIWidgetCreator.mScrollFG = 
//					hscrollbar_fg != null ? hscrollbar_fg.fullName : hscrollbar_bg.fullName;
//				
//				NGUIWidgetCreator.CreateScrollBar(owner);
//				
//				hscrollbar = Util.FindComponent<UIScrollBar>(Selection.activeGameObject);
//				hscrollbar.direction = UIScrollBar.Direction.Horizontal;
//			}
//			if (hscrollbar != null)
//			{
//				PsdLayerToNGUI.MoveAndResize(hscrollbar.gameObject, hscrollbar_bg);
//				
//				var go = hscrollbar.gameObject;
//				var c2 = hscrollbar_bg;
//				var x = (float)c2.area.left;
//				var y = (float)c2.area.top;
//				{
//					x -= (float)targetWidth/2f;
//					y -= (float)targetHeight/2f;
//					y += (float)c2.area.height/2f;
//					
//					var pos = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(-y), 0);
//					pos.z = go.transform.localPosition.z;
//					go.transform.localPosition = pos;
//				}
//			}
			
			var view = Util.FindComponent<PsdLayerVirtualView>(owner);
			if (view == null)
				return false;
			{
				view.bg = bgSprite.transform;
				
				var box = Util.FindComponent<BoxCollider>(owner, true);
				box.center = bgSprite.transform.localPosition;
				box.size = bgSprite.transform.localScale;
			}
			
			var panel = Util.FindComponent<UIPanel>(owner);
			if (panel == null)
				return false;
			{
				panel.depth = PsdLayerToNGUI.data.nguiPanelDepth++;
				panel.clipping = UIDrawCall.Clipping.SoftClip;
				panel.baseClipRegion = new Vector4(
					bgSprite.transform.localPosition.x, 
					bgSprite.transform.localPosition.y, bg.area.width, bg.area.height);
			}
			
			//**todo
//			view.verticalScrollBar = vscrollbar;
//			view.horizontalScrollBar = hscrollbar;
			
			if (item != null)
			{
				var itemGo = Util.FindGameObject(view.gameObject, "Item", true);
				
				foreach (var child in item.children)
				{
					if (!UpdateNGUI(itemGo, child))
						CreateNGUI(itemGo, child);
				}
				view.item = itemGo.transform;
				
				Util.SetXYCenterAmongChildren(itemGo);
			}
			Selection.activeGameObject = view.gameObject;
		}
		else
		{
			return false;
		}
		
		return true;
	}
	
	private static bool UpdateTexture(GameObject owner, PsdLayerCommandParser.Control c)
	{
		Selection.activeGameObject = Util.FindGameObjectRecursively(owner, c.name, true);
		if (Selection.activeGameObject == null)
			return false;

		var tex = Util.FindComponent<UITexture>(Selection.activeGameObject);
		if (tex != null)
		{
			//**todo
			//tex.mainTexture = AssetDatabase.LoadAssetAtPath(c.pa.srcFilePath, typeof(Texture2D)) as Texture2D;
			return true;
		}
		return false;
	}
	
	private static bool UpdateNGUIAnimation(GameObject owner, PsdLayerCommandParser.Control c)
	{
		Selection.activeGameObject = Util.FindGameObjectRecursively(owner, c.name, true);
		if (Selection.activeGameObject == null)
			return false;
		
		var ani = Util.FindComponent<PsdLayerSpriteAnimation>(Selection.activeGameObject);
		if (ani != null && c.children.Count > 0)
		{
			var names = new List<string>();
			for (var i=0; i<c.children.Count; ++i){
				names.Add(c.children[i].fullName);
			}
			names.Sort();
			ani.SetSprites(names.ToArray());
			ani.fps = c.fps;
			
			PsdLayerToNGUI.MoveAndResize(ani.gameObject, c.children[0]);
			return true;
		}
		return false;
	}

	#endregion
};