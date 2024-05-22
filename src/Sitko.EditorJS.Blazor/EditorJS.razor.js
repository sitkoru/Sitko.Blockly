if (!window.SitkoEditorJS) {
  window.SitkoEditorJS = {
    editors: [],
    configs: {},
    instances: {},
    timeouts: [],
    init: function (id, instance, data) {
      window.SitkoEditorJS.instances[id] = instance;
      const editorConfig = window.SitkoEditorJS.configs[id];
      editorConfig.minHeight = 0;
      editorConfig.data = data;
      editorConfig.onChange = (api, event) => {
        window.SitkoEditorJS.editors[id].save().then((outputData) => {
          if (window.SitkoEditorJS.timeouts[id]) {
            clearTimeout(window.SitkoEditorJS.timeouts[id]);
          }
          window.SitkoEditorJS.timeouts[id] = setTimeout(function () {
            console.debug(id, 'Update text', outputData);
            window.SitkoEditorJS.instances[id].invokeMethodAsync('OnSave', outputData);
            delete window.SitkoEditorJS.timeouts[id];
          }, 50)
        }).catch((error) => {
          console.log('Saving failed: ', error)
        });
      };

      // for (const [key, value] of Object.entries(config.tools)) {
      //   editorConfig.tools[key] = {
      //     class: window[value.className],
      //     config: value.config
      //   }
      // }
      console.log(editorConfig);
      window.SitkoEditorJS.editors[id] = new EditorJS(editorConfig);
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
