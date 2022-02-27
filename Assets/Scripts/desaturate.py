import os
from PIL import Image

directory = "C:\\Users\\HomeOne\\Projects\\_Sync\\GitHub\\brackeys-glitchy\\Assets\\Sprites\\"
ext = ".png"

def luma(r, g, b):
	return int(0.2126 * r + 0.7152 * g + 0.0722 * b)

def desaturate(filename, output):
	img = Image.open(filename)
	width, height = img.size
	
	for x in range(width):
		for y in range(height):
			color = img.getpixel((x, y))
			grey = luma(color[0], color[1], color[2])
			img.putpixel((x, y), (grey, grey, grey, color[3]))
	
	img.save(output)

for root, folders, files in os.walk(directory):
	for file in files:
		try:
			if file.endswith(ext) and "grey" not in file:
				pos = file.rfind(".")
				output = file[0:pos] + "_grey" + ext
				desaturate(os.path.join(root, file), os.path.join(root, output))
		except:
			print(os.path.join(root, file) + " failed!")