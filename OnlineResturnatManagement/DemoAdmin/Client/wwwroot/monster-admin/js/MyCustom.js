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


function getCrop(image, size, clipPosition = 'center-middle') {
    const width = size.width;
    const height = size.height;
    const aspectRatio = width / height;

    let newWidth;
    let newHeight;

    const imageRatio = image.width / image.height;

    if (aspectRatio >= imageRatio) {
        newWidth = image.width;
        newHeight = image.width / aspectRatio;
    } else {
        newWidth = image.height * aspectRatio;
        newHeight = image.height;
    }

    let x = 0;
    let y = 0;
    if (clipPosition === 'center-middle') {
        x = (image.width - newWidth) / 2;
        y = (image.height - newHeight) / 2;
    
    } else if (clipPosition === 'scale') {
        x = 0;
        y = 0;
        newWidth = width;
        newHeight = height;
    } else {
        console.error(
            new Error('Unknown clip position property - ' + clipPosition)
        );
    }

    return {
        cropX: x,
        cropY: y,
        cropWidth: newWidth,
        cropHeight: newHeight,
    };
}



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
//For Text
var textNode = new Konva.Text({
    text: 'Some text here',
    x: 50,
    y: 50,
    fontSize: 20,
});

//Image DragDrop
con.addEventListener('drop', function (e) {
    e.preventDefault();
    stage.setPointersPositions(e);
    
    
    Konva.Image.fromURL(itemURL, function (image) {
        image.setAttrs({
            width: 200,
            height: 200,
            name: 'image',
            draggable: true,
        });
        layer.add(image);
        /* layer.add(tr1);*/
       /* image.width(200);
        image.height(200);*/
        image.position(stage.getPointerPosition());
        /*image.draggable(true);*/
       /* image.stroke('blue');
        image.strokeWidth(5);*/

        //
        layer.add(textNode);
        //
        const tr = new Konva.Transformer({
            nodes: [image],
            keepRatio: false,
            boundBoxFunc: (oldBox, newBox) => {
                if (newBox.width < 10 || newBox.height < 10) {
                    return oldBox;
                }
                return newBox;
            },
        });

        layer.add(tr);

        image.on('transform', () => {
            // reset scale on transform
            image.setAttrs({
                scaleX: 1,
                scaleY: 1,
                width: image.width() * image.scaleX(),
                height: image.height() * image.scaleY(),
            });
            applyCrop(image.getAttr('lastCropUsed'));
        });


    });

    /*layer.on('mouseout', function (evt) {
        var shape = evt.target;
        document.body.style.cursor = 'pointer';
        shape.strokeEnabled(false);
    });
    layer.on('mouseover', function (evt) {
        var shape = evt.target;
        document.body.style.cursor = 'default';
        shape.strokeEnabled(true);
    });*/
    

   /* document
        .getElementById('delete')
        .addEventListener('click', function (e) {
            layer.remove();
        });*/
   /* layer.on('dblclick', function (e) {
        console.log(e);
        const { target } = e;
        target.remove();
        target.Transformer.remove();
        
    });
*/

    /* document
        .getElementById('save')
        .addEventListener('click', function (e) {
            state.forEach((item, index) => {
                console.log(item);
                console.log("index",index);

                *//*var node = new Konva.Image({
                    draggable: true,
                    name: 'item-' + index,
                    // make it smaller
                    scaleX: 0.5,
                    scaleY: 0.5,
                });*//*
            });
        });*/
   
});

