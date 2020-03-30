using System;
using System.Collections;
using UnityEngine;

	// Token: 0x02000007 RID: 7
	public class HackGUI : MonoBehaviour
	{
		// Token: 0x0600001F RID: 31
		public void OnGUI()
		{
			this.EspAndOthers(Vars.BlueEnemyColor, Vars.RedEnemyColor);
			GUI.Label(new Rect(10f, 0f, (float)Screen.width, 600f), string.Concat(new object[]
			{
				"FPS:",
				1f / Time.smoothDeltaTime,
				Environment.NewLine,
				"<color=red>Mod by Frosty Hacker</color>"
			}));
			GUI.color = Color.red;
			if (GUI.Button(new Rect((float)(Screen.width / 2 - 100), 8f, 200f, 20f), "Open/Close"))
			{
				this._ShowGUI = !this._ShowGUI;
			}
			if (this._ShowGUI)
			{
				GUI.backgroundColor = Color.red;
				this._GUIrect = GUI.Window(1, this._GUIrect, new GUI.WindowFunction(this.Hackwindow), "<color=white>Mod By Frosty Hacker</color>");
				if (this._ShowS)
				{
					GUI.backgroundColor = Color.red;
					this._Coterect = GUI.Window(2, this._Coterect, new GUI.WindowFunction(this.EspSettings), "<color=white>Esp Settings</color>");
				}
			}
		}

		// Token: 0x06000020 RID: 32
		private void Hackwindow(int window)
		{
			Vars.editScrollPosition = GUILayout.BeginScrollView(Vars.editScrollPosition, new GUILayoutOption[0]);
			Vars._ESPLine = GUILayout.Toggle(Vars._ESPLine, "ESP Line", new GUILayoutOption[0]);
			HackGUI._espBox = GUILayout.Toggle(HackGUI._espBox, "Esp Box", new GUILayoutOption[0]);
			HackGUI._espInfo = GUILayout.Toggle(HackGUI._espInfo, "ESP Info", new GUILayoutOption[0]);
			if (!string.IsNullOrEmpty(HackGUI.str1) && HackGUI.str1.Contains("Unlocked"))
			{
				Vars.telekill = GUILayout.Toggle(Vars.telekill, "Telekill", new GUILayoutOption[0]);
			}
			else
			{
				GUILayout.Label("<color=red>Telekill</color>\n{works on a random day}", new GUILayoutOption[0]);
			}
			if (GUILayout.Button("Esp Settings", new GUILayoutOption[0]))
			{
				this._ShowS = !this._ShowS;
			}
			GUI.DragWindow();
			GUILayout.EndScrollView();
		}

		// Token: 0x06000021 RID: 33
		public HackGUI()
		{
			this.lineStyle = 1;
			this._GUIrect = new Rect(40f, 40f, 200f, 300f);
			this._Coterect = new Rect(250f, 40f, 180f, 300f);
			this._ShowS = false;
			this._ShowGUI = false;
		}

		// Token: 0x06000022 RID: 34
		public void Update()
		{
			this.Player = GameObject.FindGameObjectWithTag("Player").transform;
			this.enemy = UnityEngine.Object.FindObjectsOfType<PlayerSkin>();
		}

		// Token: 0x06000023 RID: 35
		private static void Line(Vector2 start, Vector2 end, Color color)
		{
			Application.targetFrameRate = 100000;
			if (HackGUI.lineMat == null)
			{
				HackGUI.lineMat = new Material(Shader.Find("GUI/Text Shader"));
				HackGUI.lineMat.hideFlags = HideFlags.HideAndDontSave;
				HackGUI.lineMat.shader.hideFlags = HideFlags.HideAndDontSave;
				HackGUI.lineMat.SetInt("_SrcBlend", 3);
				HackGUI.lineMat.SetInt("_DstBlend", 3);
				HackGUI.lineMat.SetInt("_Cull", 0);
				HackGUI.lineMat.SetInt("_ZWrite", 0);
				HackGUI.lineMat.SetInt("_ZTest", 8);
			}
			HackGUI.lineMat.SetPass(0);
			GL.Begin(1);
			GL.Color(color);
			GL.Vertex3(start.x, start.y, 0f);
			GL.Vertex3(end.x, end.y, 0f);
			GL.End();
		}

		// Token: 0x06000024 RID: 36
		private static void DrawText(int fontsize, Rect pos, string info, Color color, FontStyle fontstyle)
		{
			GUI.Label(pos, info, new GUIStyle
			{
				fontSize = fontsize,
				fontStyle = fontstyle,
				normal = 
				{
					textColor = color
				}
			});
		}

		// Token: 0x06000025 RID: 37
		private void EspSettings(int window)
		{
			GUILayout.Label("Esp Line", new GUILayoutOption[0]);
			if (GUILayout.Button("Bottom", new GUILayoutOption[0]))
			{
				this.lineStyle = 1;
			}
			if (GUILayout.Button("Top", new GUILayoutOption[0]))
			{
				this.lineStyle = 2;
			}
			if (GUILayout.Button("Center", new GUILayoutOption[0]))
			{
				this.lineStyle = 3;
			}
			GUILayout.Space(5f);
			GUILayout.Label("Color", new GUILayoutOption[0]);
			Color redEnemyColor = Vars.RedEnemyColor;
			GUILayout.Label("RedTeam Color", new GUILayoutOption[0]);
			GUI.color = Color.red;
			Vars.r3 = GUILayout.HorizontalSlider(Vars.r3, 0f, 100f, new GUILayoutOption[0]);
			GUI.color = Color.green;
			Vars.g3 = GUILayout.HorizontalSlider(Vars.g3, 0f, 100f, new GUILayoutOption[0]);
			GUI.color = Color.blue;
			Vars.b3 = GUILayout.HorizontalSlider(Vars.b3, 0f, 100f, new GUILayoutOption[0]);
			Vars.RedEnemyColor = this.RGBColor(Vars.r3, Vars.g3, Vars.b3);
			GUI.color = Color.white;
			Color blueEnemyColor = Vars.BlueEnemyColor;
			GUILayout.Label("BlueTeam Color", new GUILayoutOption[0]);
			GUI.color = Color.red;
			Vars.r32 = GUILayout.HorizontalSlider(Vars.r32, 0f, 100f, new GUILayoutOption[0]);
			GUI.color = Color.green;
			Vars.g32 = GUILayout.HorizontalSlider(Vars.g32, 0f, 100f, new GUILayoutOption[0]);
			GUI.color = Color.blue;
			Vars.b32 = GUILayout.HorizontalSlider(Vars.b32, 0f, 100f, new GUILayoutOption[0]);
			Vars.BlueEnemyColor = this.RGBColor(Vars.r32, Vars.g32, Vars.b32);
			GUI.color = Color.white;
			GUI.DragWindow();
		}

		// Token: 0x06000026 RID: 38
		private IEnumerator LoadText()
		{
			WWW w = new WWW("https://pastebin.com/raw/2XEm6DHP");
			yield return w;
			HackGUI.str1 = w.text;
			yield break;
		}

		// Token: 0x06000027 RID: 39
		public void Start()
		{
			Debug.Log(Utils.test);
			base.StartCoroutine(this.LoadText());
		}

		// Token: 0x06000029 RID: 41
		private Color RGBColor(float r, float g, float b)
		{
			return new Color(r / 100f, g / 100f, b / 100f);
		}

		// Token: 0x0600002A RID: 42
		private void EspAndOthers(Color b, Color r)
		{
			for (int i = 0; i < this.enemy.Length; i++)
			{
				if (this.Player != null && this.enemy[i].isPlayerActive && Vars._ESPLine)
				{
					if (PlayerInput.instance.PlayerTeam == Team.Red)
					{
						if (this.enemy[i].PlayerTeam == Team.Blue)
						{
							Vector3 position = this.enemy[i].transform.position;
							Vector3 vector = Camera.main.WorldToScreenPoint(position);
							if (vector.z > 0f)
							{
								Vector3 vector2 = GUIUtility.ScreenToGUIPoint(vector);
								vector2.y = (float)Screen.height - vector2.y;
								if (this.lineStyle == 1)
								{
									HackGUI.Line(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 5)), vector2, b);
								}
								if (this.lineStyle == 2)
								{
									HackGUI.Line(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 15)), vector2, b);
								}
								if (this.lineStyle == 3)
								{
									HackGUI.Line(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), vector2, b);
								}
							}
						}
					}
					else if (PlayerInput.instance.PlayerTeam == Team.Blue && this.enemy[i].PlayerTeam == Team.Red)
					{
						Vector3 position2 = this.enemy[i].transform.position;
						Vector3 vector3 = Camera.main.WorldToScreenPoint(position2);
						if (vector3.z > 0f)
						{
							Vector3 vector4 = GUIUtility.ScreenToGUIPoint(vector3);
							vector4.y = (float)Screen.height - vector4.y;
							if (this.lineStyle == 1)
							{
								HackGUI.Line(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 5)), vector4, r);
							}
							if (this.lineStyle == 2)
							{
								HackGUI.Line(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 15)), vector4, r);
							}
							if (this.lineStyle == 3)
							{
								HackGUI.Line(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), vector4, r);
							}
						}
					}
				}
				if (Vars.telekill)
				{
					if (PlayerInput.instance.PlayerTeam == Team.Red)
					{
						if (this.enemy[i].PlayerTeam == Team.Blue)
						{
							this.Player.transform.position = this.enemy[i].transform.position + new Vector3(0f, 2f, 0f);
						}
					}
					else if (PlayerInput.instance.PlayerTeam == Team.Blue && this.enemy[i].PlayerTeam == Team.Red)
					{
						this.Player.transform.position = this.enemy[i].transform.position + new Vector3(0f, 2f, 0f);
					}
				}
				if (HackGUI._espBox)
				{
					Rect rect = default(Rect);
					Vector3 vector5 = this.enemy[i].transform.position;
					Vector3 vector6 = vector5;
					vector6.y += 1.8f;
					vector5 = Camera.main.WorldToScreenPoint(vector5);
					vector6 = Camera.main.WorldToScreenPoint(vector6);
					if (vector5.z > 0f && vector6.z > 0f)
					{
						Vector3 vector7 = GUIUtility.ScreenToGUIPoint(vector5);
						vector7.y = (float)Screen.height - vector7.y;
						Vector3 vector8 = GUIUtility.ScreenToGUIPoint(vector6);
						vector8.y = (float)Screen.height - vector8.y;
						float num = Math.Abs(vector7.y - vector8.y) / 2.2f;
						float num2 = num / 2f;
						rect = new Rect(vector8.x - num2, vector8.y, num, vector7.y - vector8.y);
					}
					if (PlayerInput.instance.PlayerTeam == Team.Red)
					{
						if (this.enemy[i].PlayerTeam == Team.Blue)
						{
							HackGUI.DrawRectangle(rect, b);
						}
					}
					else if (PlayerInput.instance.PlayerTeam == Team.Blue && this.enemy[i].PlayerTeam == Team.Red)
					{
						HackGUI.DrawRectangle(rect, r);
					}
				}
			}
		}

		// Token: 0x0600002B RID: 43
		private static void DrawRectangle(Rect rect, Color color)
		{
			Vector3 v = new Vector3(rect.x, rect.y, 0f);
			Vector3 v2 = new Vector3(rect.x + rect.width, rect.y, 0f);
			Vector3 v3 = new Vector3(rect.x + rect.width, rect.y + rect.height, 0f);
			Vector3 v4 = new Vector3(rect.x, rect.y + rect.height, 0f);
			HackGUI.Line(v, v2, color);
			HackGUI.Line(v2, v3, color);
			HackGUI.Line(v3, v4, color);
			HackGUI.Line(v4, v, color);
		}

		// Token: 0x0400008B RID: 139
		private Rect _GUIrect;

		// Token: 0x0400008C RID: 140
		private bool _ShowGUI;

		// Token: 0x0400008E RID: 142
		private static bool _espInfo;

		// Token: 0x0400008F RID: 143
		private static bool _espBox;

		// Token: 0x04000093 RID: 147
		public static HackGUI instane;

		// Token: 0x04000094 RID: 148
		private Rect _Coterect;

		// Token: 0x04000095 RID: 149
		private Vector2 editScrollPosition;

		// Token: 0x04000096 RID: 150
		private Transform Player;

		// Token: 0x04000097 RID: 151
		private static Material lineMat;

		// Token: 0x04000098 RID: 152
		private bool telekill;

		// Token: 0x04000099 RID: 153
		private bool _ShowS;

		// Token: 0x0400009A RID: 154
		private bool _ShowE;

		// Token: 0x0400009B RID: 155
		private int lineStyle;

		// Token: 0x0400009C RID: 156
		private bool _espBox3D;

		// Token: 0x040000A2 RID: 162
		private static string str1;

		// Token: 0x040000A3 RID: 163
		private PlayerSkin[] enemy;

		// Token: 0x040000A9 RID: 169
		public static bool _espBomb;

		// Token: 0x040000AC RID: 172
		private bool OpenClose;
	}
