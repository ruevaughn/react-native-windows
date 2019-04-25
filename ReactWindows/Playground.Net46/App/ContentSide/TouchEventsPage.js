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

  mouseDownHandler = () => {
    this.props.logger(`Touch`)
  }

  render() {
    return (
        <View style={{width: 100, height: 50}}>
        <TouchableOpacity
          style={styles.outeTouchableOpacityStyle}
          onPress={() => this.mouseDownHandler()}>
          <View pointerEvents={'box-only'}
            style={styles.innerViewStyle}
              >
            <Text>JOIN</Text>
          </View>
        </TouchableOpacity>
      </View>
    )
  }
}