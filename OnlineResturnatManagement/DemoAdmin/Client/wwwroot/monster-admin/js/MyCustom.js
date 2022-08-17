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
var text = new Konva.Text({
    x: 10,
    y: 15,
    text: 'Simple Text',
    fontSize: 30,
    fontFamily: 'Calibri',
    fill: 'green'
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

    // function to apply crop
    function applyCrop(pos) {
        const img = layer.findOne('.image');
        img.setAttr('lastCropUsed', pos);
        const crop = getCrop(
            img.image(),
            { width: img.width(), height: img.height() },
            pos
        );
        img.setAttrs(crop);
    }
    Konva.Image.fromURL(itemURL, function (image) {
        layer.add(image);
        layer.add(text);
        /* layer.add(tr1);*/
        image.width(200);
        image.height(200);
        /*image.Text("hello");*/
        image.position(stage.getPointerPosition());
        image.draggable(true);
        /* image.stroke('red');
         image.strokeWidth(10);*/

    });
   /* layer.on('mouseout', function (evt) {
        var shape = evt.target;
        document.body.style.cursor = 'default';
        shape.strokeEnabled(true);
    });*/


    /* document
         .getElementById('delete')
         .addEventListener('click', function (e) {
             layer.remove();
         });*/



    layer.on('click', function (e) {
        console.log(stage.getPointerPosition());
        console.log(e);
       /* const { target } = e;
        target.remove();*/


    });

});

