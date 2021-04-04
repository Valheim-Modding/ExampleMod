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
echo "CREATEPROJECTPATH   =" $CREATEPROJECTPATH
echo ""
echo "----------------------------REMOVE VERSION NUMBER------------------------------------------"
version=`LC_ALL=en_US.utf8 grep -Po '(?<="version_number": )".*"(?=,)' "$CREATEPROJECTPATH/manifest.json"`
echo $version
sed -i "s/$version/\"1.0.0\"/" "$CREATEPROJECTPATH/Plugin/Properties/AssemblyInfo.cs"
sed -i "s/$version/\"1.0.0\"/" "$CREATEPROJECTPATH/Plugin/Plugin.cs"