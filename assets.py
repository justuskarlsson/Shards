import os
from pathlib import Path
import shutil

MOD_NAME = "Shards"
GAME_DIR = os.environ.get("BANNERLORD_GAME_DIR", "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Mount & Blade II Bannerlord")

sln_path = Path(os.path.abspath(__file__)).parent

assets_src = Path(GAME_DIR) / "Modules" / MOD_NAME / "Assets"
assets_target = sln_path/"_Module"/"Assets"


if os.path.isdir(assets_target):
    shutil.rmtree(assets_target)
shutil.copytree(assets_src, assets_target)
