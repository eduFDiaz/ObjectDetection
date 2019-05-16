import React, { Component } from 'react';
import axios from 'axios';

export class DetectObject extends Component {
    displayName = DetectObject.name

    constructor(props) {
        super(props);
        this.state = {
            selectedFile : null, 
            items: [],
            loading: true,
        };
    }

    fileUploadHandler = () => {
        const fd = new FormData();
        fd.append('image', this.state.selectedFile, this.state.selectedFile.name);
        console.log(this.state.selectedFile);
        console.log(this.state.selectedFile.name);
        console.log(fd);
        axios.post('api/Image/Upload', fd).then(response => { console.log(response); });
    }

    fileSelectedHandler = event => {
        this.setState({ selectedFile: event.target.files[0] });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <p>The content was loaded</p>;

        return (
            <div>
                <h1>Object Detection</h1>
                <p>Load an image and see the objects detected by the yolo v2 C# wrapper.</p>
                <input type="file" onChange={this.fileSelectedHandler} /><br/>
                <button onClick={this.fileUploadHandler}>Upload Image</button><br/>
                {contents}
            </div>
        );
    }
}