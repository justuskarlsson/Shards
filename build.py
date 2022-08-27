import os
from pathlib import Path
import shutil

MOD_NAME = "Shards"
GAME_DIR = os.environ.get("BANNERLORD_GAME_DIR", "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Mount & Blade II Bannerlord")

sln_path = Path(os.path.abspath(__file__)).parent

os.system(f"dotnet build .\\{MOD_NAME}.sln")

target_path = Path(GAME_DIR) / "Modules" / MOD_NAME

src_dll = f"{sln_path}/bin/x64/Debug/net472/{MOD_NAME}.dll"
target_dll = f"{target_path}/bin/Win64_Shipping_Client/{MOD_NAME}.dll"
shutil.copy(src_dll, target_dll)

resources_path = sln_path/"_Module"

if os.path.isdir(target_path/"ModuleData"):
    shutil.rmtree(target_path/"ModuleData")
shutil.copytree(resources_path/"ModuleData", target_path/"ModuleData")
shutil.copy(resources_path/"SubModule.xml", target_path/"SubModule.xml")
