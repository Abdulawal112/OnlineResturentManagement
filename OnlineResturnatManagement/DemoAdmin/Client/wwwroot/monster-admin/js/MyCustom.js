var width = window.innerWidth;
var height = window.innerHeight;

var stage = new Konva.Stage({
    container: 'container',
    width: width,
    height: height,
});
var layer = new Konva.Layer({
    width: 150,
    height: 90,
    draggable: true,
});
stage.add(layer);

// what is url of dragging element?
var itemURL = '';
document
    .getElementById('drag-items')
    .addEventListener('dragstart', function (e) {
        itemURL = e.target.src;
    });

var con = stage.container();
con.addEventListener('dragover', function (e) {
    e.preventDefault(); // !important
});

con.addEventListener('drop', function (e) {
    e.preventDefault();
    stage.setPointersPositions(e);
    /*var tr1 = new Konva.Transformer({
        nodes: [layer],
        centeredScaling: true,
        rotationSnaps: [0, 90, 180, 270],
        resizeEnabled: false,
    });*/
   
    Konva.Image.fromURL(itemURL, function (image) {
        layer.add(image);
       /* layer.add(tr1);*/
        image.width(200);
        image.height(200);
        
        image.position(stage.getPointerPosition());
        image.draggable(true);
    });
});