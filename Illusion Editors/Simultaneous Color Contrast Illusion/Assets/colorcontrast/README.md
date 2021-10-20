# colorcontrast

This is a package that helps you generate results of simultaneous color contrast, which you may use to integrate the illusion into your games.
After download, the tool will be seen under "Window/ColorContrast".

The tool contains two part:
1.Generate with direction law
2.Generate with simultaneous color contrast

The first part of the package is used to generate the third color result when you input two of the three layers: object color, background color and perceived color. The tool will then calculate the possible color of the third layer, and show the RGB value of the result below. Pressing the "Display Result" button will then display the actual result using the object and background color, for the developer to check if the perceived color is what they expected. If not, you can try to adjust the alpha value, which will subtly change the result of the third color to help match the required value. 
Developer may also use the material section to input and output with materials. Pressing the "Apply Material Color to Editor" button will input the object material's color to the object layer, and the background material's color to background layer. Pressing the "Apply Color to Material" button will output the current result of object and background layer back to the materials above. If no material is used in either of the fields, the tool will then generate a new material under "Assets/".

The second part of the package is designed to generate colors that causes simultaneous color contrast when put adjacently, which is, the color in the center looks different upon sorroundings with different colors. Select two colors and which layer it represents, then the tool will generate the third color, just like in the first section. Pressing the "Display Result" will then display the actual result using the object and background colors that you input or generated. A slider is implemented to change the saturation value of the colors, and the developer can decide which value works the best(the difference is often seen as the largest when the saturation is around 50). The material section is also valid here, which will generate all of the three materials, including the object and the two backgrounds.
