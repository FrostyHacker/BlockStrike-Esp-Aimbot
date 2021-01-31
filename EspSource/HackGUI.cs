using System;
using UnityEngine;

namespace Cheat
{
	// Token: 0x0200001B RID: 27
	public class HackGUI : MonoBehaviour
	{
		// Token: 0x06000031 RID: 49
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

		// Token: 0x06000032 RID: 50
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

		// Token: 0x06000033 RID: 51
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

		// Token: 0x06000034 RID: 52
		private bool IsVisible(Vector3 Position)
		{
			RaycastHit raycastHit;
			return !Physics.Linecast(PlayerInput.instance.FPCamera.transform.position, Position, out raycastHit);
		}

		// Token: 0x06000035 RID: 53
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

		// Token: 0x06000036 RID: 54
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

		// Token: 0x06000037 RID: 55
		private void Esp()
		{
			for (int i = 0; i < this.enemy.Length; i++)
			{
				if (this.Player != null && this.enemy[i] != null && !this.enemy[i].Controller.photonView.isMine)
				{
					Color color = (this.enemy[i].PlayerTeam == Team.Blue) ? Color.blue : Color.red;
					if (!this.enemy[i].Dead && PlayerInput.instance.PlayerTeam != this.enemy[i].PlayerTeam)
					{
						Rect rect = default(Rect);
						Vector3 vector = this.enemy[i].transform.position;
						Vector3 localPlayerPos = this.Player.transform.position;
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
								this.DrawText(17, new Rect(rect.x, rect.top, 200f, 20f), string.Concat(new object[]
								{
									this.enemy[i].Controller.photonView.name,
									" | ",
									(int)Vector3.Distance(localPlayerPos, this.enemy[i].transform.position),
									" M"
								}), color, FontStyle.Normal);
							}
						}
						if (this._telekill)
						{
							this.Player.transform.position = this.enemy[i].transform.position + new Vector3(0f, 2f, 0f);
						}
						if (this._aimbot)
						{
							float num3 = 1000f;
							Vector3 vector7 = Camera.main.WorldToScreenPoint(this.enemy[i].transform.position);
							float num4 = Mathf.Abs(Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(vector7.x, (float)Screen.height - vector7.y)));
							if (num4 <= this._aimbotfov && num4 < num3)
							{
								if (this._aimVisibleOnly)
								{
									if (this.IsVisible(this.enemy[i].transform.position))
									{
										this.LookAtEnemy(this.enemy[i].transform, this._aimbotspeed, this._aimbotY);
									}
								}
								else
								{
									this.LookAtEnemy(this.enemy[i].transform, this._aimbotspeed, this._aimbotY);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000038 RID: 56
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

		// Token: 0x06000039 RID: 57
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

		// Token: 0x0600003A RID: 58
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

		// Token: 0x0600003C RID: 60
		public void Update()
		{
			if (PhotonNetwork.inRoom)
			{
				this.Player = GameObject.FindGameObjectWithTag("Player").transform;
				this.enemy = UnityEngine.Object.FindObjectsOfType<PlayerSkin>();
			}
		}

		// Token: 0x06000106 RID: 262
		public void LookAtEnemy(Transform enemy, float aimSpeed, float aimY)
		{
			Quaternion to = Quaternion.LookRotation((enemy.position + new Vector3(0f, aimY, 0f) - PlayerInput.instance.FPCamera.transform.position).normalized);
			PlayerInput.instance.FPCamera.transform.rotation = Quaternion.Slerp(PlayerInput.instance.FPCamera.transform.rotation, to, Time.deltaTime * aimSpeed);
			PlayerInput.instance.FPCamera.SetRotation(PlayerInput.instance.FPCamera.transform.eulerAngles, true, true);
		}

		// Token: 0x04000088 RID: 136
		private Rect _GUIrect;

		// Token: 0x04000089 RID: 137
		private bool _ShowGUI;

		// Token: 0x0400008A RID: 138
		private bool _espInfo;

		// Token: 0x0400008B RID: 139
		private bool _espBox;

		// Token: 0x0400008C RID: 140
		public static HackGUI instane;

		// Token: 0x0400008D RID: 141
		private Rect _Coterect;

		// Token: 0x0400008E RID: 142
		private Vector2 editScrollPosition;

		// Token: 0x0400008F RID: 143
		private Transform Player;

		// Token: 0x04000090 RID: 144
		private bool _ShowS;

		// Token: 0x04000091 RID: 145
		private bool _ShowE;

		// Token: 0x04000092 RID: 146
		private int lineStyle;

		// Token: 0x04000094 RID: 148
		private bool OpenClose;

		// Token: 0x04000095 RID: 149
		private bool _telekill;

		// Token: 0x04000096 RID: 150
		private bool _espLine;

		// Token: 0x04000097 RID: 151
		private bool _aimbot;

		// Token: 0x04000098 RID: 152
		private Material LineMat;

		// Token: 0x04000099 RID: 153
		public float _aimbotfov;

		// Token: 0x0400009A RID: 154
		public float _aimbotspeed;

		// Token: 0x0400009B RID: 155
		public bool _aimVisibleOnly;

		// Token: 0x0400009C RID: 156
		public float _aimbotY;

		// Token: 0x04000239 RID: 569
		private PlayerSkin[] enemy;
	}
}
