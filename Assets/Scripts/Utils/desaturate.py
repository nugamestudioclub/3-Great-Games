import os
from PIL import Image
import traceback

directory = "C:\\Users\\Executor\\Projects\\GitHub\\brackeys-glitchy\\Assets\\Sprites"
ext = ".png"

def luma(r, g, b):
	return int(0.2126 * r + 0.7152 * g + 0.0722 * b)

def level(value, avg):
	max_value = 255
	baseline = 0.5
	
	modifier = 1 if avg == 0 else baseline * max_value / avg;
	
	return value if modifier <= 1 else int(value * modifier)

def desaturate(filename, output):
	img = Image.open(filename)
	width, height = img.size
	
	sum = 0
	
	for x in range(width):
		for y in range(height):
			color = img.getpixel((x, y))
			grey = luma(color[0], color[1], color[2])
			sum += grey
			
			img.putpixel((x, y), (grey, grey, grey, color[3]))
			
	avg_brightness = sum / (width * height)
		
	for x in range(width):
		for y in range(height):
			color = img.getpixel((x, y))
			new_color = min(255, level(color[0], avg_brightness))
			img.putpixel((x, y), (new_color, new_color, new_color, color[3]))
	
	img.save(output)

for root, folders, files in os.walk(directory):
	finished = 0
	count = 0
	for file in files:
		try:
			if file.endswith(ext) and "grey" not in file:
				count += 1
				pos = file.rfind(".")
				output = file[0:pos] + "_grey" + ext
				desaturate(os.path.join(root, file), os.path.join(root, output))
				finished += 1
		except Exception as ex:
			print(ex)
			print(os.path.join(root, file) + " failed!")
	print(f"{finished} out of {count} finished successfully")