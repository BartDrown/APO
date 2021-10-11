using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO {
    public class HistogramService {
        public HistogramView histogramView;
        private ImageService imageService;

        public HistogramService(ImageService imageService) {
            this.imageService = imageService;
        }

        public void renderHistogram(string type) {
            this.histogramView = new HistogramView(this.imageService);
            this.histogramView.renderHistogram(type);
        }


    }
}
