using System;
using UnityEngine;

namespace Cheat
{
	// Token: 0x0200001B RID: 27
	public class HackGUI : MonoBehaviour
	{
		// Token: 0x06000132 RID: 306
		public void OnGUI()
		{
			GUI.color = Color.red;
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
					this._Coterect = GUI.Window(2, this._Coterect, new GUI.WindowFunction(this.AimBotSettings), "<color=white>AimBot Settings</color>");
				}
			}
			this.Esp();
			if (this._aimbot)
			{
				this.DrawCircle(new Vector2((float)(Screen.width / 2), (float)Screen.height / 2f), this._aimbotfov, Color.cyan);
			}
		}

		// Token: 0x06000133 RID: 307
		private void Hackwindow(int window)
		{
			this.editScrollPosition = GUILayout.BeginScrollView(this.editScrollPosition, new GUILayoutOption[0]);
			this._espLine = GUILayout.Toggle(this._espLine, "ESP Line", new GUILayoutOption[0]);
			this._espBox = GUILayout.Toggle(this._espBox, "ESP Box", new GUILayoutOption[0]);
			this._espInfo = GUILayout.Toggle(this._espInfo, "ESP Info", new GUILayoutOption[0]);
			this._telekill = GUILayout.Toggle(this._telekill, "TeleKill", new GUILayoutOption[0]);
			this._aimbot = GUILayout.Toggle(this._aimbot, "AimBot", new GUILayoutOption[0]);
			if (GUILayout.Button("AimBot Settings", new GUILayoutOption[0]))
			{
				this._ShowS = !this._ShowS;
			}
			GUI.DragWindow();
			GUILayout.EndScrollView();
		}

		// Token: 0x06000134 RID: 308
		public HackGUI()
		{
			this._aimbotspeed = 8f;
			this.lineStyle = 1;
			this._GUIrect = new Rect(40f, 40f, 200f, 300f);
			this._Coterect = new Rect(250f, 40f, 180f, 300f);
			this._ShowS = false;
			this._ShowGUI = false;
			this._aimbotfov = 50f;
			this._aimbotY = 1f;
		}

		// Token: 0x06000135 RID: 309
		private bool IsVisible(Vector3 Position)
		{
			RaycastHit raycastHit;
			return !Physics.Linecast(PlayerInput.instance.FPCamera.transform.position, Position, out raycastHit);
		}

		// Token: 0x06000136 RID: 310
		private void DrawLine(Vector2 start, Vector2 end, Color color)
		{
			Application.targetFrameRate = 100000;
			if (this.LineMat == null)
			{
				this.LineMat = new Material(Shader.Find("GUI/Text Shader"));
				this.LineMat.hideFlags = HideFlags.HideAndDontSave;
				this.LineMat.shader.hideFlags = HideFlags.HideAndDontSave;
				this.LineMat.SetInt("_SrcBlend", 3);
				this.LineMat.SetInt("_DstBlend", 3);
				this.LineMat.SetInt("_Cull", 0);
				this.LineMat.SetInt("_ZWrite", 0);
				this.LineMat.SetInt("_ZTest", 8);
			}
			this.LineMat.SetPass(0);
			GL.Begin(1);
			GL.Color(color);
			GL.Vertex3(start.x, start.y, 0f);
			GL.Vertex3(end.x, end.y, 0f);
			GL.End();
		}

		// Token: 0x06000137 RID: 311
		private void DrawRectangle(Rect rect, Color color)
		{
			Vector3 v = new Vector3(rect.x, rect.y, 0f);
			Vector3 v2 = new Vector3(rect.x + rect.width, rect.y, 0f);
			Vector3 v3 = new Vector3(rect.x + rect.width, rect.y + rect.height, 0f);
			Vector3 v4 = new Vector3(rect.x, rect.y + rect.height, 0f);
			this.DrawLine(v, v2, color);
			this.DrawLine(v2, v3, color);
			this.DrawLine(v3, v4, color);
			this.DrawLine(v4, v, color);
		}

		// Token: 0x06000138 RID: 312
		private void Esp()
		{
			for (int i = 0; i < this.enemy.Length; i++)
			{
				if (this.Player != null && this.enemy[i] != null)
				{
					Color color = (this.enemy[i].PlayerTeam == Team.Blue) ? Color.blue : Color.red;
					if (!this.enemy[i].Dead && PlayerInput.instance.PlayerTeam != this.enemy[i].PlayerTeam)
					{
						Rect rect = default(Rect);
						Vector3 vector = this.enemy[i].transform.position;
						Vector3 vector2 = vector;
						vector2.y += 1.8f;
						vector = Camera.main.WorldToScreenPoint(vector);
						vector2 = Camera.main.WorldToScreenPoint(vector2);
						if (vector.z > 0f && vector2.z > 0f)
						{
							Vector3 vector3 = GUIUtility.ScreenToGUIPoint(vector);
							vector3.y = (float)Screen.height - vector3.y;
							Vector3 vector4 = GUIUtility.ScreenToGUIPoint(vector2);
							vector4.y = (float)Screen.height - vector4.y;
							float num = Math.Abs(vector3.y - vector4.y) / 2.2f;
							float num2 = num / 2f;
							rect = new Rect(vector4.x - num2, vector4.y, num, vector3.y - vector4.y);
							if (this._espBox)
							{
								this.DrawRectangle(rect, color);
							}
							if (this._espLine)
							{
								Vector3 position = this.enemy[i].transform.position;
								Vector3 vector5 = Camera.main.WorldToScreenPoint(position);
								if (vector5.z > 0f)
								{
									Vector3 vector6 = GUIUtility.ScreenToGUIPoint(vector5);
									vector6.y = (float)Screen.height - vector6.y;
									this.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 5)), vector6, color);
								}
							}
							if (this._espInfo)
							{
								Vector3 position2 = this.Player.transform.position;
								Vector3 position3 = this.enemy[i].transform.position;
								Vector3 position4 = this.enemy[i].transform.position + new Vector3(0f, 2f, 0f);
								Vector3 vector7 = Camera.main.WorldToScreenPoint(position4);
								if (vector7.z > 0f)
								{
									this.DrawText(17, new Rect(vector7.x, (float)Screen.height - vector7.y, 200f, 20f), string.Concat(new object[]
									{
										this.enemy[i].Controller.photonView.name,
										" | ",
										(int)Vector3.Distance(position2, position3),
										" M"
									}), color, FontStyle.Normal);
								}
							}
						}
						if (this._telekill)
						{
							this.Player.transform.position = this.enemy[i].transform.position + new Vector3(0f, 2f, 0f);
						}
						if (this._aimbot)
						{
							float num3 = 1000f;
							Vector3 vector8 = Camera.main.WorldToScreenPoint(this.enemy[i].transform.position);
							float num4 = Mathf.Abs(Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector8.x, (float)Screen.height - vector8.y)));
							if (num4 <= this._aimbotfov && num4 < num3)
							{
								if (this._aimVisibleOnly)
								{
									if (this.IsVisible(this.enemy[i].transform.position))
									{
										this.LookAtEnemy(this.enemy[i], this._aimbotspeed, this._aimbotY);
									}
								}
								else
								{
									this.LookAtEnemy(this.enemy[i], this._aimbotspeed, this._aimbotY);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000139 RID: 313
		public void DrawCircle(Vector2 Pos, float radius, Color color)
		{
			double pi = 3.1415926535;
			for (double num2 = 0.0; num2 < 360.0; num2 += 1.0)
			{
				double value = (double)radius * Math.Cos(num2 * pi / 180.0);
				double value2 = (double)radius * Math.Sin(num2 * pi / 180.0);
				this.DrawLine(new Vector2(Pos.x + Convert.ToSingle(value), Pos.y + Convert.ToSingle(value2)), new Vector2(Pos.x + 1f + Convert.ToSingle(value), Pos.y + 1f + Convert.ToSingle(value2)), color);
			}
		}

		// Token: 0x0600013A RID: 314
		private void DrawText(int fontsize, Rect pos, string info, Color color, FontStyle fontstyle)
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

		// Token: 0x0600013B RID: 315
		private void AimBotSettings(int window)
		{
			GUILayout.Label(string.Format("Aimbot Fov: {0}", this._aimbotfov), new GUILayoutOption[0]);
			this._aimbotfov = GUILayout.HorizontalSlider(this._aimbotfov, 0f, 380f, new GUILayoutOption[0]);
			GUILayout.Label(string.Format("Aimbot Speed: {0}", this._aimbotspeed), new GUILayoutOption[0]);
			this._aimbotspeed = GUILayout.HorizontalSlider(this._aimbotspeed, 1f, 7f, new GUILayoutOption[0]);
			GUILayout.Label(string.Format("Aimbot Y: {0}", this._aimbotY), new GUILayoutOption[0]);
			this._aimbotY = GUILayout.HorizontalSlider(this._aimbotY, 0.5f, 1.5f, new GUILayoutOption[0]);
			this._aimVisibleOnly = GUILayout.Toggle(this._aimVisibleOnly, "Check Visible", new GUILayoutOption[0]);
			GUI.DragWindow();
		}

		// Token: 0x0600013C RID: 316
		public void LookAtEnemy(PlayerSkin enemy, float aimSpeed, float aimY)
		{
			Quaternion to = Quaternion.LookRotation((enemy.transform.position + new Vector3(0f, aimY, 0f) - PlayerInput.instance.FPCamera.transform.position).normalized);
			PlayerInput.instance.FPCamera.transform.rotation = Quaternion.Slerp(PlayerInput.instance.FPCamera.transform.rotation, to, Time.deltaTime * aimSpeed);
			PlayerInput.instance.FPCamera.SetRotation(PlayerInput.instance.FPCamera.transform.eulerAngles, true, true);
		}

		// Token: 0x0600013D RID: 317
		public void Update()
		{
			this.Player = GameObject.FindGameObjectWithTag("Player").transform;
			this.enemy = UnityEngine.Object.FindObjectsOfType<PlayerSkin>();
		}

		// Token: 0x04000294 RID: 660
		private Rect _GUIrect;

		// Token: 0x04000295 RID: 661
		private bool _ShowGUI;

		// Token: 0x04000296 RID: 662
		private bool _espInfo;

		// Token: 0x04000297 RID: 663
		private bool _espBox;

		// Token: 0x04000298 RID: 664
		public static HackGUI instane;

		// Token: 0x04000299 RID: 665
		private Rect _Coterect;

		// Token: 0x0400029A RID: 666
		private Vector2 editScrollPosition;

		// Token: 0x0400029B RID: 667
		private Transform Player;

		// Token: 0x0400029C RID: 668
		private bool _ShowS;

		// Token: 0x0400029D RID: 669
		private bool _ShowE;

		// Token: 0x0400029E RID: 670
		private int lineStyle;

		// Token: 0x0400029F RID: 671
		private PlayerSkin[] enemy;

		// Token: 0x040002A0 RID: 672
		private bool OpenClose;

		// Token: 0x040002A1 RID: 673
		private bool _telekill;

		// Token: 0x040002A2 RID: 674
		private bool _espLine;

		// Token: 0x040002A3 RID: 675
		private bool _aimbot;

		// Token: 0x040002A4 RID: 676
		private Material LineMat;

		// Token: 0x040002A5 RID: 677
		public float _aimbotfov;

		// Token: 0x040002A6 RID: 678
		public float _aimbotspeed;

		// Token: 0x040002A7 RID: 679
		public bool _aimVisibleOnly;

		// Token: 0x040002A8 RID: 680
		public float _aimbotY;
	}
}
