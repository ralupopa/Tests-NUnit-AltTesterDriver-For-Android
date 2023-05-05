echo "==> Uninstalling the app from the device..."
adb uninstall com.Altom.TrashCat

echo "==> Installing the app on the device..."
adb install app/TrashCat.apk

echo "==> Setup ADB port forwarding..."
adb forward --remove-all 
adb forward tcp:13000 tcp:13000

echo " Start the app "

adb shell am start -n com.Altom.TrashCat/com.unity3d.player.UnityPlayerActivity


echo "==> Wait for app to start"
sleep 5
cd TestsAltTrashCatCSharp

echo "==> Restore test project and run tests"
dotnet test  -- NUnit.TestOutputXml = "TestAlttrashCSharp"

echo "==> Kill app"
adb shell am force-stop com.Altom.TrashCat