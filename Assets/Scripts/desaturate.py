import os
from PIL import Image

directory = "C:\\Users\\HomeOne\\Projects\\_Sync\\GitHub\\brackeys-glitchy\\Assets\\Sprites\\Platformer\\block"
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
			img.putpixel((x, y), (grey, grey, grey))
	
	img.save(output)

for root, folders, files in os.walk(directory):
	for file in files:
		if file.endswith(ext):
			pos = file.rfind(".")
			output = file[0:pos] + "_grey" + ext
			desaturate(os.path.join(root, file), os.path.join(root, output))