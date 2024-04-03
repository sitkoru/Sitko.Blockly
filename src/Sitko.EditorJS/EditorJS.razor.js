if (!window.SitkoEditorJS) {
  window.SitkoEditorJS = {
    editors: [],
    timeouts: [],
    init: function (id, configJson, instance) {
      const config = JSON.parse(configJson) ?? {};
      const editorConfig = {
        holder: config.holder,
        tools: {},
        minHeight : 0,
        onChange: (api, event) => {
          window.SitkoEditorJS.editors[id].save().then((outputData) => {
            if (window.SitkoEditorJS.timeouts[id]) {
              clearTimeout(window.SitkoEditorJS.timeouts[id]);
            }
            window.SitkoEditorJS.timeouts[id] = setTimeout(function () {
              //console.debug(id, 'Update text');
              instance.invokeMethodAsync('OnSave', JSON.stringify(outputData));
              delete window.SitkoEditorJS.timeouts[id];
            }, 50)
          }).catch((error) => {
            console.log('Saving failed: ', error)
          });
        }
      };
      for (const [key, value] of Object.entries(config.tools)) {
        editorConfig.tools[key] = {
          class: window[value.className],
          config: value.config
        }
      }
      console.log(config);
      console.log(editorConfig);
      window.SitkoEditorJS.editors[id] = new EditorJS(editorConfig);
      //console.debug('CKEditor config', id, config);
      // window[editorClass]
      //   .create(element, config)
      //   .then(editor => {
      //     window.SitkoEditorJS.editors[id] = editor;
      //     editor.model.document.on('change:data', () => {
      //       if (window.SitkoEditorJS.timeouts[id]) {
      //         clearTimeout(window.SitkoEditorJS.timeouts[id]);
      //       }
      //       window.SitkoEditorJS.timeouts[id] = setTimeout(function () {
      //         //console.debug(id, 'Update text');
      //         instance.invokeMethodAsync('UpdateText', editor.getData());
      //         delete window.SitkoEditorJS.timeouts[id];
      //       }, 50)
      //
      //     });
      //   })
      //   .catch(error => {
      //     //console.error('Error initializing CKEditor', error);
      //   });
    },
    // update: function (id, content) {
    //   if (this.editors.hasOwnProperty(id)) {
    //     var editor = this.editors[id];
    //     var oldData = editor.getData();
    //     if (oldData !== content) {
    //       //console.debug(id, 'Update value', oldData, content);
    //       editor.setData(content);
    //     }
    //   }
    // },
    destroy: function (id) {
      // if (this.editors.hasOwnProperty(id)) {
      //   //console.debug('Destroy editor');
      //   this.editors[id].destroy();
      //   delete this.editors[id];
      // }
    }
  }
}
