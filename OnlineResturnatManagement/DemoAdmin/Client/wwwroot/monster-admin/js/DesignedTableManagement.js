

/*console.log('hello=>', { TableManagementJson });*/
//var json =
//    '{"attrs":{"width":578,"height":200},"className":"Stage","children":[{"attrs":{},"className":"Layer","children":[{"attrs":{"width":"auto","height":"auto","text":"Text Shadow!","fontFamily":"Calibri","fontSize":95,"x":20,"y":20,"stroke":"red","strokeWidth":2,"shadowColor":"black","shadowBlur":2,"shadowOffsetX":10,"shadowOffsetY":10,"shadowOpacity":0.5},"className":"Text"},{"attrs":{"stroke":"green","strokeWidth":10,"lineJoin":"round","lineCap":"round","points":[{"x":50,"y":140},{"x":450,"y":160}],"shadowColor":"black","shadowBlur":10,"shadowOffsetX":5,"shadowOffsetY":5,"shadowOpacity":0.5},"className":"Line"},{"attrs":{"x":280,"y":100,"width":100,"height":50,"fill":"#00D2FF","stroke":"black","strokeWidth":4,"shadowColor":"black","shadowBlur":10,"shadowOffsetX":5,"shadowOffsetY":5,"shadowOpacity":0.5,"rotation":0.35038503988659,"id":"blueRectangle"},"className":"Rect"},{"attrs":{"x":100,"y":41,"width":106,"height":118,"id":"yodaImage"},"className":"Image"}]}]}';

//var stage = Konva.Node.create(TableManagementJson, 'container');

///*
// * set functions
// */
//stage.findOne('#blueRectangle').on('mouseover mouseout', function () {
//    var stroke = this.stroke();
//    this.stroke(stroke === 'black' ? 'red' : 'black');
//});
///*
// * set images
// */
//var imageObj = new Image();
//imageObj.onload = function () {
//    stage.findOne('#yodaImage').image(imageObj);
//};
//imageObj.src = '/assets/yoda.jpg';