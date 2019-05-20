import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
    View,
    Animated,
    TouchableWithoutFeedback,
    StyleSheet
} from 'react-native'
import styles from './styles'
import images from '../Images'

const SHORT_DURATION = 100

export default class Animations extends Component {
    static propTypes = {
        logger: PropTypes.func
    }

    constructor(props) {
        super(props)

        this.state = {
            size: new Animated.Value(32),
            borderRadius: new Animated.Value(16),
            imageSize: new Animated.Value(19)
        }
    }

    hoverStart = () => {
        this.props.logger('hover start')
        Animated.parallel([
            Animated.timing(this.state.size, { toValue: 35, duration: SHORT_DURATION }),
            Animated.timing(this.state.borderRadius, { toValue: 19, duration: SHORT_DURATION }),
            Animated.timing(this.state.imageSize, { toValue: 21, duration: SHORT_DURATION })
        ]).start(() => {
            this.isAnimating = false
        })
    }

    hoverEnd = () => {

        this.props.logger('hover end')

        Animated.parallel([
            Animated.timing(this.state.size, { toValue: 32, duration: SHORT_DURATION }),
            Animated.timing(this.state.borderRadius, { toValue: 16, duration: SHORT_DURATION }),
            Animated.timing(this.state.imageSize, { toValue: 19, duration: SHORT_DURATION })
        ]).start(() => {
            this.isAnimating = false
        })
    }

    draw = (color) => {
        let figures = []
        for (let i = 0; i < 1000; i++) {
            figures.push(<View key={i} style={{ ...localStyles.rectangle, borderColor: color }} />)
        }

        return figures
    }

    renderShapes() {
        const { color } = this.state
        return (
            <View style={localStyles.container}>
                {this.draw(color)}
            </View>
        );
    }

    render() {
        return (
            <View style={{flexDirection: 'row'}}>
                <Animated.View style={[styles.container, { width: this.state.size, height: this.state.size }]}
                    onMouseEnter={this.hoverStart}
                    onMouseLeave={this.hoverEnd}>
                    <TouchableWithoutFeedback>
                        <Animated.Image
                            style={{
                                width: this.state.imageSize,
                                height: this.state.imageSize
                            }}
                            source={images.bjnLogo} />
                    </TouchableWithoutFeedback>
                </Animated.View>
                <Animated.View style={[styles.container, { width: this.state.size, height: this.state.size }]}
                    onMouseEnter={this.hoverStart}
                    onMouseLeave={this.hoverEnd}>
                    <TouchableWithoutFeedback>
                        <Animated.Image
                            style={{
                                width: this.state.imageSize,
                                height: this.state.imageSize
                            }}
                            source={images.bjnLogo} />
                    </TouchableWithoutFeedback>
                </Animated.View>
                <Animated.View style={[styles.container, { width: this.state.size, height: this.state.size }]}
                    onMouseEnter={this.hoverStart}
                    onMouseLeave={this.hoverEnd}>
                    <TouchableWithoutFeedback>
                        <Animated.Image
                            style={{
                                width: this.state.imageSize,
                                height: this.state.imageSize
                            }}
                            source={images.bjnLogo} />
                    </TouchableWithoutFeedback>
                </Animated.View>
                <Animated.View style={[styles.container, { width: this.state.size, height: this.state.size }]}
                    onMouseEnter={this.hoverStart}
                    onMouseLeave={this.hoverEnd}>
                    <TouchableWithoutFeedback>
                        <Animated.Image
                            style={{
                                width: this.state.imageSize,
                                height: this.state.imageSize
                            }}
                            source={images.bjnLogo} />
                    </TouchableWithoutFeedback>
                </Animated.View>
                {/* {this.renderShapes()} */}
            </View>
        )
    }

}

const localStyles = StyleSheet.create({
    container: {
      flex: 1,
      flexWrap: 'wrap',
      justifyContent: 'center',
      alignItems: 'center',
      backgroundColor: '#F5FCFF',
    },
    rectangle: {
      width: 10,
      height: 10,
      borderWidth: 1,
      borderColor: 'red',
      backgroundColor: 'green',
    }
  });