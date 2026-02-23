/* NRA 50 meter reduced for 50 yard Target */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freETarget.targets {
    [Serializable]
    class NRA_A27 : aTarget {

        private decimal pelletCaliber;// = 5.6m; //.22LR
        private const bool solidInnerTenRing = false;  // TRUE if the inner ring painted solid white   
        private const decimal targetSize = 220; //mm

        private const int trkZoomMin = 0;
        private const int trkZoomMax = 5;
        private const int trkZoomVal = 0;
        private const double zoomStep = 0.48;
        private const decimal pdfZoomFactor = 0.29m;

        private const decimal innerRing = 9.1186m; //mm
        private const decimal ring10 = 18.2626m; //mm
        private const decimal ring9 = 36.5506m; //mm
        private const decimal ring8 = 54.8386m; //mm
        private const decimal ring7 = 73.1266m; //mm
        private const decimal ring6 = 91.4146m; //mm
        private const decimal ring5 = 109.7026m; //mm
        private const decimal outterRing = 127.9652m; //mm

        private const int blackRings = 6;  // Largest Black Ring
        private const decimal blackCircle = 98.806m; //mm
        private const int firstRing = 4;  // Largest Ring

        private decimal innerTenRadius;// = innerRing / 2m + pelletCaliber / 2m;
        private decimal r10, r9, r8, r7, r6, r5, r4;

        private static readonly decimal[] rings = new decimal[] { outterRing, ring5, ring6, ring7, ring8, ring9, ring10, innerRing };


        public NRA_A27(decimal caliber) : base(caliber) {
            this.pelletCaliber = caliber;
            innerTenRadius = innerRing / 2m + pelletCaliber / 2m;
            r10 = ring10 / 2m + pelletCaliber / 2m;
            r9 = ring9 / 2m + pelletCaliber / 2m;
            r8 = ring8 / 2m + pelletCaliber / 2m;
            r7 = ring7 / 2m + pelletCaliber / 2m;
            r6 = ring6 / 2m + pelletCaliber / 2m;
            r5 = ring5 / 2m + pelletCaliber / 2m;
            r4 = outterRing / 2m + pelletCaliber / 2m;
        }

        public override decimal getScore(decimal radius) {
            if (radius <= 0) {
                return 10.9m;
            } else if (radius <= r10) {
                return 10.9m - (radius / r10) * 0.9m;
            } else if (radius <= r9) {
                return 10m - ((radius - r10) / (r9 - r10));
            } else if (radius <= r8) {
                return 9m - ((radius - r9) / (r8 - r9));
            } else if (radius <= r7) {
                return 8m - ((radius - r8) / (r7 - r8));
            } else if (radius <= r6) {
                return 7m - ((radius - r7) / (r6 - r7));
            } else if (radius <= r5) {
                return 6m - ((radius - r6) / (r5 - r6));
            } else if (radius <= r4) {
                return 5m - ((radius - r5) / (r4 - r5));
            } else {
                return 0;
            }
        }

        public override int getBlackRings() {
            return blackRings;
        }

        public override decimal getInnerTenRadius() {
            return innerTenRadius;
        }

        public override decimal getOutterRadius() {
            return getOutterRing() / 2m + pelletCaliber / 2m;
        }

        public override decimal get10Radius() {
            return ring10 / 2m + pelletCaliber / 2m;
        }

        public override string getName() {
            return typeof(NRA_A27).FullName;
        }

        public override decimal getOutterRing() {
            return outterRing;
        }

        public override decimal getProjectileCaliber() {
            return pelletCaliber;
        }

        public override decimal[] getRings() {
            return rings;
        }

        public override decimal getSize() {
            return targetSize;
        }

        public override decimal getZoomFactor(int value) {
            return (decimal)(1 / Math.Pow(2, value * zoomStep));
        }

        public override bool isSolidInner() {
            return solidInnerTenRing;
        }

        public override int getTrkZoomMaximum() {
            return trkZoomMax;
        }

        public override int getTrkZoomMinimum() {
            return trkZoomMin;
        }

        public override int getTrkZoomValue() {
            return trkZoomVal;
        }

        public override float getFontSize(float diff) {
            //return diff / 6f;
            return 10;
        }

        public override decimal getBlackDiameter() {
            return blackCircle;
        }

        public override int getRingTextCutoff() {
            return 9;
        }

        public override float getTextOffset(float diff, int ring) {
            return 0;
        }

        public override decimal getPDFZoomFactor(List<Shot> shotList) {
            if (shotList == null) {
                return pdfZoomFactor;
            } else {
                bool zoomed = true;
                foreach (Shot s in shotList) {
                    if (s.score < 6) {
                        zoomed = false;
                    }
                }

                if (zoomed) {
                    return 0.5m;
                } else {
                    return 1;
                }
            }
        }


        public override int getTextRotation() {
            return 0;
        }

        public override int getFirstRing() {
            return firstRing;
        }

        public override (decimal, decimal) rapidFireBarDimensions() {
            return (-1, -1);
        }

        public override bool drawNorthText() {
            return false;
        }

        public override bool drawSouthText() {
            return false;
        }

        public override bool drawWestText() {
            return true;
        }

        public override bool drawEastText() {
            return false;
        }
    }
}
