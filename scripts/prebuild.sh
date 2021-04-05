[ -z "$STANDALONEUNITYPATH" ] && STANDALONEUNITYPATH="/c/Program Files/Unity/Editor/Unity.exe"
[ -z "$UNITYHUBEDITORPATH" ] && UNITYHUBEDITORPATH="/c/Program Files/Unity/Hub/Editor/2019.4.20f1/Editor/Unity.exe"
# use next line if you installed Unity separatly (but note, that you should use 2019.4.20f1 version)
# [ -z "$UNITYPATH" ] && UNITYPATH=STANDALONEUNITYPATH

# TODO: find a way how to use declared UNITYPATH variable in GH
# use next line if you installed Unity using Unity Hub
UNITYPATH=$UNITYHUBEDITORPATH

# get path to directory where script is located
MY_PATH="`dirname \"$0\"`"
# get absolute path to solution itself
CREATEPROJECTPATH="`( cd \"$MY_PATH/..\" && pwd )`"
if [ -z "$CREATEPROJECTPATH" ] ; then
  # error; for some reason, the path is not accessible
  # to the script (e.g. permissions re-evaled after suid)
  exit 1  # fail
fi

echo "--------------------------------CALCULATED DATA--------------------------------------------"
# echo calculated variables for easier script debug
echo "STANDALONEUNITYPATH =" $STANDALONEUNITYPATH
echo "UNITYHUBEDITORPATH  =" $UNITYHUBEDITORPATH
echo "UNITYPATH           =" $UNITYPATH
echo "CREATEPROJECTPATH   =" $CREATEPROJECTPATH
echo ""
echo "-----------------------------PUT VERSION NUMBER--------------------------------------------"
version=`LC_ALL=en_US.utf8 grep -Po '(?<="version_number": )".*"(?=,)' "$CREATEPROJECTPATH/manifest.json"`
echo $version
sed -i "s/\"1.0.0\"/$version/" "$CREATEPROJECTPATH/Plugin/Properties/AssemblyInfo.cs"
sed -i "s/\"1.0.0\"/$version/" "$CREATEPROJECTPATH/Plugin/Plugin.cs"
echo ""
echo "----------------------------GENERATE ASSET BUNDLE------------------------------------------"
# execute our pipeline script in batch mode of Unity
exec "$UNITYPATH" -batchmode -quit -projectPath "$CREATEPROJECTPATH/Unity Project" -executeMethod Pipeline.BuildAssetBundles -nographics -stackTraceLogType Full -disable-gpu-skinning