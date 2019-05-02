import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  TouchableOpacity,
  View,
} from 'react-native'

import styles from './styles'

export default class TouchEvents extends Component {
  static propTypes = {
    logger: PropTypes.func
  }

  mouseDownHandler = (touchSource) => {
    this.props.logger(touchSource)
  }

  render() {
    return (
        <View style={{width: 100, height: 150, flex: 1}}>
        <TouchableOpacity
          style={styles.outTouchableOpacityStyle}
          onPress={() => this.mouseDownHandler(`Touch from box-only`)}>
          <View pointerEvents={'box-only'}
            style={styles.innerViewStyle}
              >
            <Text>Box-only</Text>
          </View>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.outTouchableOpacityStyle}
          onPress={() => this.mouseDownHandler(`Touch from box-none`)}>
          <View pointerEvents={'box-none'}
            style={styles.innerViewStyle}
              >
            <Text>Box-none</Text>
          </View>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.outTouchableOpacityStyle}
          onPress={() => this.mouseDownHandler(`Touch from auto`)}>
          <View
            style={styles.innerViewStyle}
              >
            <Text>Auto</Text>
          </View>
        </TouchableOpacity>
      </View>
    )
  }
}