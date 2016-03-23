/* 
AutoBuilder.cs
Automatically changes the target platform and creates a build.
 
Installation
Place in an Editor folder.
 
Usage
Go to File > AutoBuilder and select a platform. These methods can also be run from the Unity command line using -executeMethod AutoBuilder.MethodName.
 
License
Copyright (C) 2011 by Thinksquirrel Software, LLC
 
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
 
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
 
public static class AutoBuilder {
 
  static string GetProjectName()
  {
    string[] s = Application.dataPath.Split('/');
    return s[s.Length - 2];
  }
 
  static string[] GetScenePaths()
  {
    List<string> EditorScenes = new List<string>();

    foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
    {
      if (scene.enabled)
      {
        EditorScenes.Add(scene.path);
      }
    }

    return EditorScenes.ToArray();
  }
 
  static void GenericBuild(string[] scenes, string targetDirectory, BuildTarget buildTarget, BuildOptions buildOptions)
  {
    EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
    string result = BuildPipeline.BuildPlayer(scenes, targetDirectory, buildTarget, buildOptions);
    if (result.Length > 0) {
      throw new Exception("BuildPlayer failure: " + result);
    }
  }

  [MenuItem("File/AutoBuilder/Windows/32-bit")]
  static void PerformWinBuild ()
  {
    GenericBuild(GetScenePaths(), "Builds/Win/" + GetProjectName() + ".exe", BuildTarget.StandaloneWindows, BuildOptions.None);
  }
 
  [MenuItem("File/AutoBuilder/Windows/64-bit")]
  static void PerformWin64Build ()
  {
    GenericBuild(GetScenePaths(), "Builds/Win64/" + GetProjectName() + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
  }
 
  [MenuItem("File/AutoBuilder/Mac OSX/Universal")]
  static void PerformOSXUniversalBuild ()
  {
    GenericBuild(GetScenePaths(), "Builds/OSX-Universal/" + GetProjectName() + ".app", BuildTarget.StandaloneOSXUniversal, BuildOptions.None);
  }

  [MenuItem("File/AutoBuilder/Linux/Universal")]
  static void PerformLinuxUniversalBuild ()
  {
    GenericBuild(GetScenePaths(), "Builds/Linux-Universal/" + GetProjectName(), BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);
  }

  [MenuItem("File/AutoBuilder/iOS")]
  static void PerformiOSBuild ()
  {
    GenericBuild(GetScenePaths(), "Builds/iOS", BuildTarget.iOS, BuildOptions.None);
  }

  [MenuItem("File/AutoBuilder/Android")]
  static void PerformAndroidBuild ()
  {
    GenericBuild(GetScenePaths(), "Builds/Android", BuildTarget.Android, BuildOptions.None);
  }

  [MenuItem("File/AutoBuilder/WebGL")]
  static void PerformWebBuild ()
  {
    GenericBuild(GetScenePaths(), "Builds/WebGL/", BuildTarget.WebGL, BuildOptions.None);
  }
}