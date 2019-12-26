import * as V from "victory";
import React from "react";
import data from "../CompanyDashboard/data";
import { VictoryBar, VictoryChart, VictoryAxis, VictoryTheme } from "victory";

class BarChart extends React.Component {
	static defaultProps = {
		height: 250,
		width: 250,
		domainPadding: 20,
		horizontal: false,
		data, // {name: "", value: ""} pairs
		angle: 45,
		labelPadding: 40,
		fontSize: 14
	};
	render() {
		const { height, width, domainPadding, horizontal, data, angle, labelPadding, fontSize } = this.props;
		return (
			<div style={{ height: height + "px", width: width + "px" }}>
				<VictoryChart
					// domainPadding will add space to each side of VictoryBar to
					// prevent it from overlapping the axis
					domainPadding={domainPadding}
					theme={VictoryTheme.material}
				>
					<VictoryAxis
						// tickValues specifies both the number of ticks and where
						// they are placed on the axis
						tickFormat={data.map((x) => x.name)}
						style={{
							axisLabel: { angle: angle },
							tickLabels: {
								angle: angle,
								padding: labelPadding,
								alignSelf: "flex-start",
								fontSize: fontSize
							}
						}}
					/>
					<VictoryAxis
						dependentAxis

						// tickFormat specifies how ticks should be displayed
						// tickFormat={x}
					/>
					<VictoryBar
						animate={{
							duration: 2000,
							onLoad: { duration: 1000 }
						}}
						data={data}
						x="name"
						y="value"
						horizontal={horizontal}
					/>
				</VictoryChart>
			</div>
		);
	}
}

export default BarChart;
